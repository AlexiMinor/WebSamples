using System.Reflection;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApp.Data;
using WebApp.WebAPI.Filters;
using WebApp.WebAPI.Infrastructure;

namespace WebApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(); // Add this line to register controller services.

            builder.Configuration.AddJsonFile("AFINN-ru.json");
            builder.Services.AddDbContext<ArticleAggregatorContext>(
                opt =>
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddSerilog();
            
            builder.Services.RegisterArticlesAggregatorServices();
            
            builder.Services.RegisterMediator();
            builder.Services.RegisterMappers();
            builder.Services.RegisterJwtAuthorization(builder.Configuration);

            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire"))
            
                );
            builder.Services.AddHangfireServer();
            builder.Services.AddHangfireServer();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "WebApp API",
                    Version = "v1",
                    Description = "WebApp API"
                });

                opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = [new HangfireDashboardAuthorizationAttribute("Admin")]
            });
            app.MapControllers();

            app.Run();
        }


    }
}
