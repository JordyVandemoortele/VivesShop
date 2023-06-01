using Microsoft.EntityFrameworkCore;
using VivesShop.Models;

namespace VivesShop.Core
{
    public class VivesShopDbContext : DbContext
    {
        public VivesShopDbContext(DbContextOptions<VivesShopDbContext> options) : base(options) 
        {
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.AssignedProducts)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired(true);

            base.OnModelCreating(modelBuilder);
        }
        public void Seed()
        {
            ProductCategories.Add(new ProductCategory
            {
                Name = "Frieten",
                Id = 1
            });
            ProductCategories.Add(new ProductCategory
            {
                Name = "Snacks",
                Id = 2
            });
            Products.Add(new Product
            {
                Name = "Klein",
                Price = 2.9M,
                Id = 1,
                CategoryId = 1
            });
            Products.Add(new Product
            {
                Name = "middel",
                Price = 3.5M,
                Id = 2,
                CategoryId = 1
            });
            Products.Add(new Product
            {
                Name = "groot",
                Price = 4.0M,
                Id = 3,
                CategoryId = 1
            });
            Products.Add(new Product
            {
                Name = "Frikandel",
                Price = 2.5M,
                Id = 4,
                CategoryId = 2
            });
            SaveChanges();
        }
    }
}
