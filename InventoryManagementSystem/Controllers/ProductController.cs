using AutoMapper;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null) return BadRequest();

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
        {
            if (productDto == null)
                return BadRequest();

            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound(); 
            }

            // تحديث الخصائص التي يمكن تعديلها
            //existingProduct.Name = product.Name;
            //existingProduct.Description = product.Description;
            //existingProduct.Quantity = product.Quantity;
            //existingProduct.Price = product.Price;
            //existingProduct.LowStockThreshold = product.LowStockThreshold;
            _mapper.Map(productDto, existingProduct);


            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.CompleteAsync();

            return Ok(existingProduct);  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null)
            {
                
                return NotFound();
            }

           
            _unitOfWork.Products.Delete(existingProduct);

           
            await _unitOfWork.CompleteAsync();

           
            return NoContent();
        }

        [HttpGet("GetProductDetails/{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
           
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(); 
            }

            var productDetails = new
            {
                product.Id,
                product.Name,
                product.Description,
                product.Quantity,
                product.Price,
                product.LowStockThreshold,
             
                Transactions = product.Transactions 
            };

            return Ok(productDetails);
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
           
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }



    }
}
