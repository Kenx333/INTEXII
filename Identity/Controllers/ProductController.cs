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

        public IActionResult ProductDetails(int id)
        {
            var product = _repo.Products.FirstOrDefault(p => p.product_ID == id);
            if (product == null)
            {
                return NotFound(); // Returns a 404 Not Found error if the product with the specified ID is not found.
            }
            return View(product); // Assumes you have a view named "ProductDetails.cshtml" to display the product details.
        }


    }
}
