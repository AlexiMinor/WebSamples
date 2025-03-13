using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Identity.MVC.Data;

public class AppDbContext : IdentityDbContext<MyCustomUser, IdentityRole<Guid>, Guid>
{
    public DbSet<DataModel> DataModels { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    
}

public class DataModel
{
    public Guid Id { get; set; }
    public string Data { get; set; }
}

public class MyCustomUser : IdentityUser<Guid>
{
    public string MyCustomProperty { get; set; }
}