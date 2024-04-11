namespace INTEXII.Models.ViewModels
{
    public class ProductsListViewModel
    {
        //create an IQueryable object to store the books
        public IQueryable<Product> Products { get; set; }
        //create a PaginationInfo object to store the pagination info
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public string? CurrentCategory { get; internal set; }
    }
}
