using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Configurations;
using PaymentGateway.Infrastructure.Configurations;
using PaymentGateway.Infrastructure.DataAccess;

namespace PaymentGateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DbContext, DataStorageDbContext>(o =>
            {
                o.UseInMemoryDatabase("InMemoryDataStorage");
            });

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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