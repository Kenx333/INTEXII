using INTEXII.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEXII.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace INTEXII.Controllers
{
    public class HomeController : Controller
    {
        //private UserManager<AppUser> userManager;
        //public HomeController(UserManager<AppUser> userMgr)
        //{
        //    userManager = userMgr;
        //}

        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.Products.Take(3).ToList();
            return View(products);
        }
        //[Authorize]
        ////[Authorize(Roles = "Manager")]
        //public async Task<IActionResult> Index()
        //{
        //    AppUser user = await userManager.GetUserAsync(HttpContext.User);
        //    string message = "Hello " + user.UserName;
        //    return View((object)message);
        //}

        public async Task<IActionResult> Privacy()
        {
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> About()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        public async Task<IActionResult> ShoppingCart()
        {
            return View();
        }


    }
}