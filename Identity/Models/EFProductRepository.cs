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
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }




        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }




        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
