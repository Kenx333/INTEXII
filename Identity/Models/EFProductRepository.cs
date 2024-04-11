using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace INTEXII.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ProductContext _context;
        public EFProductRepository(ProductContext temp)
        {
            _context = temp;
        }
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<LineItem> LineItems => _context.LineItems;

        public void AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
