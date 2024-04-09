namespace INTEX2.Models
{
    public class IProductRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<Customer> Customers { get; }
        public IQueryable<LineItem> LineItems { get; }
    }
}
