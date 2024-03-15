using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using TennisPlanet.Infrastructure.Data.Domain;

namespace TennisPlanet.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          Database.EnsureCreated();
        }
      public DbSet<Brand> Brands { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Order> Orders { get; set; }
       public DbSet<Product> Products { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
       public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
    }
}