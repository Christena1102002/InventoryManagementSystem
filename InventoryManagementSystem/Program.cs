using InventoryManagementSystem.ViewModels;
using InventoryManagementSystem.data;
using InventoryManagementSystem.Reposatories.implementation;
using InventoryManagementSystem.Reposatories.interfaces;
using InventoryManagementSystem.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;

using InventoryManagementSystem.Models;
using InventoryManagementSystem.DTOs;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;
namespace InventoryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<InventoryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            //__________________

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });


            /***************** Interfaces injection *****************/
            builder.Services.AddScoped<IInventoryTransactionIRepository, InventoryTransactionsRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductWarehouseStockRepository, ProductWareHouseStockRepository>();
            builder.Services.AddScoped<IWareHouseRepository, WareHouseRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
