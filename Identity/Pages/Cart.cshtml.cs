using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using INTEXII.Infrastructure;
using INTEXII.Models;

namespace INTEXII.Pages
{
    public class CartModel : PageModel
    {
        private IProductRepository _repo;

        public CartModel(IProductRepository temp, Cart cartService) 
        {
            _repo = temp;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int productId, string returnUrl) 
        {
            Product? product = _repo.Products
                .FirstOrDefault(x => x.product_ID == productId);

            if (product != null)
            {
                Cart.AddItem(product, 1);
            }

            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.product_ID == productId).Product);

            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
