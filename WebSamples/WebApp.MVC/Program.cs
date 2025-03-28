using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using WebApp.Data;
using WebApp.Data.CQS.Commands;
using WebApp.MVC.Filters;
using WebApp.MVC.Middlewares;
using WebApp.MVC.RouteConstraints;
using WebApp.Services.Abstract;
using WebApp.Services.Implementations;
using WebApp.Services.Mappers;
using WebApp.Services.Samples;

namespace WebApp.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

         
            //builder.Configuration
            //    .AddInMemoryCollection(new Dictionary<string, string>()
            //    {
            //        {"name","Bob"}
            //    });

            // Add services to the container.
            builder.Services.AddControllersWithViews();//opt =>
            //{
            //    opt.Filters.Add<WhiteSpaceRemoverAttribute>();
            //    opt.Filters.Add<CheckParametersAttribute>();
            //});
            //builder.Services.Configure<RouteOptions>(opt =>
            //    opt.ConstraintMap.Add("secretCode", typeof(SecretCodeConstraint)));

            builder.Services.AddDbContext<ArticleAggregatorContext>(
                opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddSerilog();
            builder.Services.AddScoped<IAuthorsService, AuthorsService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<ISourceService, SourceService>();
            builder.Services.AddScoped<IRssService, RssService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddScoped<IScopedService, ScopedService2>();
            builder.Services.AddSingleton<ISingletonService, SingletonService>();

            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddMediatR(sc => 
                sc.RegisterServicesFromAssembly(typeof(AddArticlesCommand).Assembly));
            builder.Services.AddTransient<ArticleMapper>();
            builder.Services.AddTransient<UserMapper>();


            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire"))

            );
            builder.Services.AddHangfireServer();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt=>
                {
                    opt.LoginPath = "/account/login";
                    opt.AccessDeniedPath = "/account/accessdenied";
                    opt.LogoutPath = "/account/logout";
                });
            builder.Services.AddAuthorization();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.Map("/", () => "Hello there");
            app.MapControllerRoute(
                name: "default",
                //pattern: "{token:secretCode(234)}/{controller=Home}/{action=Index}/{id?}"
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            app.UseHangfireDashboard();

            //app.MapControllerRoute(
            //    "myCustomRouting",
            //    "{action=Index}/{controller=Home}/{name?}"
            //    );

            app.Run();
        }
    }
}
