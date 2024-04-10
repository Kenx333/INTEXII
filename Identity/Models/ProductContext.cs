using Microsoft.EntityFrameworkCore;

namespace INTEXII.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Product> Products { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<LineItem> LineItems { get; set;}
        public DbSet<Order> Orders { get; set;}
    }
}
