using Microsoft.CodeAnalysis;

namespace INTEXII.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IQueryable<Product> Products { get; set; }

        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();

        public string? CurrentProductCategory { get; set; }
    }
}
