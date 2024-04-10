using INTEXII.Models;
using INTEXII.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using System.Diagnostics;

namespace INTEXII.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repo;

        public ProductController(IProductRepository temp)
        {
            _repo = temp;
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

         

        //public IActionResult ProductDetail(int id)
        //{
        //    var product = _repo.Products
        //        .Include(x => x.LineItems)
        //        .FirstOrDefault(x => x.productID == id);

        //    return View(product);
        //}

        public IActionResult ProductList(int pageNum, string? productCategory)
        {
            int pageSize = 5;
            var products = new ProductsListViewModel
            {
                Products = _repo.Products
                .Where(x => x.category == productCategory || productCategory == null)
                .OrderBy(x => x.name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = productCategory == null ? _repo.Products.Count() : _repo.Products.Where(x => x.category == productCategory).Count()
                },
                CurrentProductCategory = productCategory
            };

            return View(products);
        }
    }
}
