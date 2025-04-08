using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Data.CQS.Commands;
using WebApp.Services.Abstract;
using WebApp.Services.Implementations;
using WebApp.Services.Mappers;

namespace WebApp.WebAPI.Infrastructure;

public static class ServiceCollectionExtenstion
{
    /// <summary>
    /// Register all services for the Articles Aggregator
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterArticlesAggregatorServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ISourceService, SourceService>();
        services.AddScoped<IRssService, RssService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IRateService, RateService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IHtmlRemoverService, HtmlRemoverService>();

        return services;
    }

    /// <summary>
    /// Register CQS for the Articles Aggregator
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterMediator(this IServiceCollection services)
    {
        services.AddMediatR(sc =>
            sc.RegisterServicesFromAssembly(typeof(AddArticlesCommand).Assembly));
        
        return services;
    }
    
    public static IServiceCollection RegisterMappers(this IServiceCollection services)
    {
        services.AddTransient<ArticleMapper>();
        services.AddTransient<UserMapper>();
        
        return services;
    }

    /// <summary>
    /// Register JWT authorization
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtIss = configuration["Jwt:Iss"];
        var jwtAud = configuration["Jwt:Aud"];
        var jwtKey = configuration["Jwt:Secret"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
        services.AddAuthorization();
        return services;
    }
}