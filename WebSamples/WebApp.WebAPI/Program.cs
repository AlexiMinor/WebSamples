using System.Reflection;
using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebApp.Data;
using WebApp.Data.CQS.Commands;
using WebApp.Services.Abstract;
using WebApp.Services.Implementations;
using WebApp.Services.Mappers;

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

            var jwtIss = builder.Configuration["Jwt:Iss"];
            var jwtAud = builder.Configuration["Jwt:Aud"];
            var jwtKey = builder.Configuration["Jwt:Secret"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIss,
                        ValidAudience = jwtAud,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });
            builder.Services.AddAuthorization();

            builder.Services.AddSerilog();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<ISourceService, SourceService>();
            builder.Services.AddScoped<IRssService, RssService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRateService, RateService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IHtmlRemoverService, HtmlRemoverService>();

            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire"))
            
                );
            builder.Services.AddHangfireServer();

            builder.Services.AddMediatR(sc =>
                sc.RegisterServicesFromAssembly(typeof(AddArticlesCommand).Assembly));
            builder.Services.AddTransient<ArticleMapper>();
            builder.Services.AddTransient<UserMapper>();

            //builder.Services.AddAuthentication();
            //builder.Services.AddAuthorization();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHangfireDashboard();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}
