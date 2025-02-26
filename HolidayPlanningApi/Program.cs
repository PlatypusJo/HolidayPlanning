
using BLL.Intefaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HolidayPlanningApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            // реализация InMemory db
            builder.Services.AddSingleton<HolidayPlanningDbContext>(provider =>
            {
                var options = new DbContextOptionsBuilder<HolidayPlanningDbContext>()
                    .UseInMemoryDatabase("HolidayDb")
                    .Options;
                return new HolidayPlanningDbContext(options);
            });
            builder.Services.AddScoped<IHolidayService, HolidayService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
