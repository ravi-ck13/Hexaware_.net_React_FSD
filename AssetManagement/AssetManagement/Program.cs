using AssetManagement.Data;
using AssetManagement.Services.Interfaces;
using AssetManagement.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace AssetManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            builder.Services.AddDbContext<EFCoreDbContext>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IAssetService, AssetService>();
            builder.Services.AddScoped<IAssetCategoryService, AssetCategoryService>();
            builder.Services.AddScoped<IEmployeeAssetAllocationService, EmployeeAssetAllocationService>();
            builder.Services.AddScoped<IAssetAuditService, AssetAuditService>();
            builder.Services.AddScoped<IAssetServiceRequestService, AssetServiceRequestService>();
            builder.Services.AddScoped<ILoginHistoryService, LoginHistoryService>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
