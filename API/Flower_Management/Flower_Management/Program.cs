using Flower_Management.Data; // Đảm bảo import đúng namespace cho DbContext
using Flower_Management.Services; // Đảm bảo import đúng namespace cho service
using Microsoft.EntityFrameworkCore;

namespace Flower_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Đăng ký DbContext với chuỗi kết nối từ appsettings.json
            builder.Services.AddDbContext<FlowerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));

            // Đăng ký IFlowerService và FlowerService
            builder.Services.AddScoped<IFlowerService, FlowerService>();

            // Thêm dịch vụ controllers
            builder.Services.AddControllers();

            // Thêm dịch vụ CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Thêm dịch vụ Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Hiển thị thông báo lỗi chi tiết trong môi trường phát triển
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Chuyển hướng đến trang lỗi cho môi trường sản xuất
            }

            app.UseHttpsRedirection();
            app.UseCors("MyCors"); // Áp dụng chính sách CORS
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
