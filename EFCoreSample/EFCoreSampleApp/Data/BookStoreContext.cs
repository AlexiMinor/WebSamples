using EFCoreSampleApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSampleApp.Data;

public class BookStoreContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Author> Authors{ get; set; }
    public DbSet<Book> Books{ get; set; }
    public DbSet<BookAuthor> BookAuthors{ get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-JPGDIHT;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}