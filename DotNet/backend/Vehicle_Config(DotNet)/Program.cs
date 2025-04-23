using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Vehicle_Config_DotNet_.ExceptionMiddleware;
using Vehicle_Config_DotNet_.Repositories;
using Vehicle_Config_DotNet_.Services;
using Newtonsoft.Json.Serialization;
using Vehicle_Config_DotNet_.Configuration;
using Vehicle_Config_DotNet_.Logging;
//using  Vehicle_Config_DotNet_.ExceptionMiddleWare;

namespace Vehicle_Config_DotNet_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                })
                .AddJsonOptions(options =>
                {
                    // Configure JSON options to handle circular references
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    // Uncomment if you need to adjust max depth
                    // options.JsonSerializerOptions.MaxDepth = 32;
                });
            // 1?? Register DbContext FIRST (before logging)
            builder.Services.AddDbContext<ProjectContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29))));

            // 2?? Register DbLoggerProvider after DbContext
            builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();

            // 3?? Configure logging
            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                logging.AddProvider(new DbLoggerProvider(builder.Services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>()));
            });


            builder.Services.AddTransient<IManufacturerService, ManufacturerService>();
            builder.Services.AddTransient<IModelService, ModelService>();
            builder.Services.AddTransient<ISegmentService, SegmentService>();
            builder.Services.AddTransient<IInvoiceService, InvoiceService>();
            builder.Services.AddTransient<IUserRepository, UserRepositoryImpl>();
            builder.Services.AddTransient<IUserRepository, UserRepositoryImpl>();
            builder.Services.AddTransient<IVehicleDetailService, VehicleDetailService>();
            builder.Services.AddTransient<CustomMiddleware>(); // Register Middleware
            builder.Services.AddTransient<ComponentDetailService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            builder.Services.AddSingleton(emailSettings);
            builder.Services.AddDbContext<ProjectContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29))));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()  // Allows all origins (replace with specific domains in production)
                           .AllowAnyMethod()  // Allows any HTTP method (GET, POST, etc.)
                           .AllowAnyHeader(); // Allows any headers
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware.CustomMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}