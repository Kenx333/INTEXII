namespace INTEXII.Models
{

    public interface IProductRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<Customer> Customers { get; }
        public IQueryable<LineItem> LineItems { get; }
        public IQueryable<Userbase> Userbased { get; }
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void RemoveProduct(Product product);
        //public void RemoveOrder(Product product);
        //public void RemoveCustomer(Product product);
    }

}
