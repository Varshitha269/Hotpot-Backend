using HotpotLibrary.Data;
using log4net.Config;
using log4net;
using Microsoft.EntityFrameworkCore;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Repository;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using HotpotLibrary.NotificationService;
using HotpotLibrary.DTO;
using HotpotLibrary.Service;
using Asp.Versioning;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            });


            // Add services to the container.
            var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Logging
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            // Register repositories and services in the DI container
            builder.Services.AddScoped<IAdminDashboard, AdminDashboardRepository>();
            builder.Services.AddScoped<AdminDashboardService>();
            builder.Services.AddScoped<ICheckoutMediator, CheckoutMediator>();
            builder.Services.AddScoped<CheckoutService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<RestaurantService>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<MenuService>();
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<MenuItemService>();

            builder.Services.AddScoped<IOrderInterface, OrderRepository>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<IOrderDetailInterface, OrderDetailsRepository>();
            builder.Services.AddScoped<OrderDetailsService>();
            builder.Services.AddScoped<IPaymentInterface, PaymentRepository>();
            builder.Services.AddScoped<PaymentsService>();
            builder.Services.AddScoped<IFeedbackRatingInterface, FeedbackRatingRepository>();
            builder.Services.AddScoped<FeedbackRatingsService>();
            builder.Services.AddScoped<ICartInterface, CartRepository>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<IRestaurantMenuItem, RestaurantMenuItemRepository>();
            builder.Services.AddScoped<RestaurantMenuItemService>();
            builder.Services.AddScoped<IRestaurantMenuItem, RestaurantMenuItemRepository>();
            builder.Services.AddScoped<RestaurantMenuItemService>();

            // Register service report interfaces and services (careful with circular dependencies)
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<ReportService>();

            builder.Services.AddScoped<IEmailService, GmailService>();

            // Register the Notification Service
            builder.Services.AddScoped<NotificationService>();

            builder.Services.AddScoped<IReportsService, ReportsService>();

            builder.Services.AddScoped<ActionFilter>();
            builder.Services.AddControllers(options =>
            {
                // Add ExecutionTimeFilter globally
                options.Filters.Add<ActionFilter>();
            });

            // Register the GlobalExceptionFilter
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>(); // Add the global exception filter here
            });



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Add Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "API V2", Version = "v2" });

            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
