using Microsoft.CodeAnalysis;

namespace INTEX2.Models
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
    }
}
