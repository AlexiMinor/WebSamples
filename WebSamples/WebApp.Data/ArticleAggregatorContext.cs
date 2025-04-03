using Microsoft.EntityFrameworkCore;
using WebApp.Data.Entities;

namespace WebApp.Data
{
    public class ArticleAggregatorContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Source?> Sources { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ArticleAggregatorContext(DbContextOptions<ArticleAggregatorContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
