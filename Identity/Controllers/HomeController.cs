using INTEXII.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using INTEXII.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace INTEXII.Controllers
{
    public class HomeController : Controller
    {
        //private UserManager<AppUser> userManager;
        //public HomeController(UserManager<AppUser> userMgr)
        //{
        //    userManager = userMgr;
        //}

        private readonly IProductRepository _repo;
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;

        public HomeController(IProductRepository productRepository, IHostEnvironment hostEnvironment)
        {
            _repo = productRepository;

            _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "fraud_model.onnx");

            //initialize the InferenceSession
            _session = new InferenceSession(_onnxModelPath);
        }

        public IActionResult Index()
        {
            var products = _repo.Products.Take(3).ToList();
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