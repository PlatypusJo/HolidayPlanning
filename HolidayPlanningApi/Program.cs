
using BLL.Intefaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Providers;
using DAL.Repositories;
using Google.Api;
using Microsoft.AspNetCore.Identity;
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
                    builder.WithOrigins(
                        "http://localhost:3000",
                        "https://sharik2468.github.io"
                    )
                .AllowAnyHeader()
                .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();

            // Init
            string path = AppDomain.CurrentDomain.BaseDirectory + @"holidayplanning-da398-firebase-adminsdk-fbsvc-5c8115c79c.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            // ���������� InMemory db
            builder.Services.AddSingleton<HolidayPlanningDbContext>(provider =>
            {
                var options = new DbContextOptionsBuilder<HolidayPlanningDbContext>()
                    .UseInMemoryDatabase("HolidayDb")
                    .Options;
                return new HolidayPlanningDbContext(options);
            });
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HolidayPlanningDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<IHolidayService, HolidayService>();
            builder.Services.AddScoped<IContractorService, ContractorService>();
            builder.Services.AddScoped<IContractorCategoryService, ContractorCategoryService>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IHolidayExpenseService, HolidayExpenseService>();
            builder.Services.AddScoped<IContractorStatusService, ContractorStatusService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IMemberCategoryService, MemberCategoryService>();
            builder.Services.AddScoped<IMemberStatusService, MemberStatusService>();
            builder.Services.AddScoped<IMenuCategoryService, MenuCategoryService>();
            builder.Services.AddScoped<IGoalService, GoalService>();
            builder.Services.AddScoped<IGoalStatusService, GoalStatusService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            ContractorStatusProvider.RegisterAll();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
