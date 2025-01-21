using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.MVC.RouteConstraints;
using WebApp.Services.Abstract;
using WebApp.Services.Implementations;
using WebApp.Services.Samples;

namespace WebApp.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Configuration
            //    .AddInMemoryCollection(new Dictionary<string, string>()
            //    {
            //        {"name","Bob"}
            //    });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.Configure<RouteOptions>(opt =>
            //    opt.ConstraintMap.Add("secretCode", typeof(SecretCodeConstraint)));

            builder.Services.AddDbContext<ArticleAggregatorContext>(
                opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IAuthorsService, AuthorsService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddScoped<IScopedService, ScopedService2>();
            builder.Services.AddSingleton<ISingletonService, SingletonService>();

            builder.Services.AddScoped<ITestService, TestService>();
            
            //builder.Services.AddScoped<BookService>();
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

            app.UseAuthorization();
            //app.Map("/", () => "Hello there");
            app.MapControllerRoute(
                name: "default",
                //pattern: "{token:secretCode(234)}/{controller=Home}/{action=Index}/{id?}"
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            //app.MapControllerRoute(
            //    "myCustomRouting",
            //    "{action=Index}/{controller=Home}/{name?}"
            //    );

            app.Run();
        }
    }
}
