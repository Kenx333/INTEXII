using Microsoft.CodeAnalysis;

namespace INTEXII.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public IQueryable<Product> Products { get; set; }

        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();

        public string? CurrentProductCategory { get; set; }
    }
}
