using AutoMapper;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<TransferStockDto, InventoryTransaction>()
           .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => TransactionType.Transfer))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow))

            .ForMember(dest => dest.SourceWarehouseId, opt => opt.MapFrom(src => src.SourceWarehouseId))
            .ForMember(dest => dest.TargetWarehouseId, opt => opt.MapFrom(src => src.TargetWarehouseId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
          
            CreateMap<InventoryTransaction, TransactionHistoryReportDto>()
               .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name))
               .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Product.Price))
               .ForMember(d => d.TransactionDate, opt => opt.MapFrom(s => s.Date));
        }
    }
}
