using AutoMapper;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.DTOs;
namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryTransactionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //⦁	Add Stock: Increase the quantity of a specific product
        [HttpPut("add-stock/{id}")]
        public async Task<IActionResult> AddStock(int id, [FromBody] AddStockDTO addStockDto)
        {
            if (addStockDto == null || addStockDto.Quantity <= 0)
                return BadRequest("Invalid quantity");


            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);

            if (existingProduct == null)
                return NotFound();

            existingProduct.Quantity += addStockDto.Quantity;

            var transaction = new InventoryTransaction
            {
                ProductId = existingProduct.Id,
                TransactionType = TransactionType.Add,
                Quantity = addStockDto.Quantity,
                Date = DateTime.UtcNow,
                UserId = "currentUserId" // مؤقتًا قيمة ثابتة أو تجيبيها من المستخدم الحالي
            };

            await _unitOfWork.InventoryTransactions.AddAsync(transaction);


            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.CompleteAsync();


            return Ok(existingProduct);
        }


        //⦁	Remove Stock: Decrease the quantity of a specific product.
        [HttpPut("remove-stock/{id}")]
        public async Task<IActionResult> RemoveStock(int id, [FromBody] AddStockDTO addStockDto)
        {
            if (addStockDto == null || addStockDto.Quantity <= 0)
                return BadRequest("Invalid quantity");

            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound("Product not found");

            if (existingProduct.Quantity < addStockDto.Quantity)
                return BadRequest("Not enough stock to remove the specified quantity.");

            existingProduct.Quantity -= addStockDto.Quantity;

            var transaction = new InventoryTransaction
            {
                ProductId = existingProduct.Id,
                TransactionType = TransactionType.Remove,  // Remove لأننا بنخصم
                Quantity = addStockDto.Quantity,
                Date = DateTime.UtcNow,
                UserId = "currentUserId" // هنفترض حالياً تكتبي Id يدوي أو تجيبيه من User.Identity
            };

            await _unitOfWork.InventoryTransactions.AddAsync(transaction);

            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.CompleteAsync();

            return Ok(existingProduct);
        }

        //⦁	Transfer Stock: Transfer stock between warehouses

        [HttpPost("transfer-stock")]
        public async Task<IActionResult> TransferStock([FromBody] TransferStockDto transferStockDto)
        {
            if (transferStockDto == null || transferStockDto.Quantity <= 0)
                return BadRequest("Invalid transfer details.");

            var sourceWarehouse = await _unitOfWork.WareHouses.GetByIdAsync(transferStockDto.SourceWarehouseId);
            var targetWarehouse = await _unitOfWork.WareHouses.GetByIdAsync(transferStockDto.TargetWarehouseId);

            if (sourceWarehouse == null || targetWarehouse == null)
                return BadRequest("One or both warehouses not found.");

            var sourceStock = await _unitOfWork.ProductWarehouseStocks
                .GetByProductAndWarehouseAsync(transferStockDto.ProductId, transferStockDto.SourceWarehouseId);

            if (sourceStock == null || sourceStock.Quantity < transferStockDto.Quantity)
                return BadRequest("Insufficient stock in source warehouse.");

            // خصم من المخزن المصدر
            sourceStock.Quantity -= transferStockDto.Quantity;

            // إضافة للمخزن الهدف
            var targetStock = await _unitOfWork.ProductWarehouseStocks
                .GetByProductAndWarehouseAsync(transferStockDto.ProductId, transferStockDto.TargetWarehouseId);

            if (targetStock == null)
            {
                targetStock = new ProductWareHouseStock
                {
                    ProductId = transferStockDto.ProductId,
                    WareHouseId = transferStockDto.TargetWarehouseId,
                    Quantity = transferStockDto.Quantity
                };
                await _unitOfWork.ProductWarehouseStocks.AddAsync(targetStock);
            }
            else
            {
                targetStock.Quantity += transferStockDto.Quantity;
            }

            // إنشاء المعاملة باستخدام AutoMapper
            var transaction = _mapper.Map<InventoryTransaction>(transferStockDto);
            transaction.TransactionType = TransactionType.Transfer;
            transaction.Date = DateTime.UtcNow;

            await _unitOfWork.InventoryTransactions.AddAsync(transaction);

            // حفظ التغييرات
            await _unitOfWork.CompleteAsync();

            return Ok("Stock transferred successfully.");
        }


    }
}