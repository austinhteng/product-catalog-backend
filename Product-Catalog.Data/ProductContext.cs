using Microsoft.EntityFrameworkCore;
using Product_Catalog.Models.Entities;

namespace Product_Catalog.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product", "dbo").HasKey(k => k.Id);
        }
    }
}
