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
        //private UserManager<Customer> userManager;
        //public HomeController(UserManager<Customer> userMgr)
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
        //    Customer user = await userManager.GetUserAsync(HttpContext.User);
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

        public IActionResult ReviewOrders()
        {
            var records = _repo.Orders
                .OrderByDescending(o => o.date)
                .Take(20)
                .ToList();
            var predictions = new List<OrderPrediction>();

            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraud"},
                { 1, "Fraud" }
            };

            foreach (var record in records)
            {
                var january1_2022 = new DateTime(2022, 1, 1);

                DateTime? newDate = DateTime.Parse(record.date);

                var daysSinceJan12022 = newDate.HasValue ? Math.Abs((newDate.Value - january1_2022).Days) : 0;

                var input = new List<float>
                {
                    (float)record.transaction_ID,
                    (float)record.customer_ID,
                    (float)record.time,
                    (float)(record.amount),

                    daysSinceJan12022,

                    record.day_of_week == "Mon" ? 1 : 0,
                    record.day_of_week == "Sat" ? 1 : 0,
                    record.day_of_week == "Sun" ? 1 : 0,
                    record.day_of_week == "Thu" ? 1 : 0,
                    record.day_of_week == "Tue" ? 1 : 0,
                    record.day_of_week == "Wed" ? 1 : 0,

                    record.entry_mode == "PIN" ? 1 : 0,
                    record.entry_mode == "Tap" ? 1 : 0,

                    record.type_of_transaction == "Online" ? 1 : 0,
                    record.type_of_transaction == "POS" ? 1 : 0,

                    record.country_of_transaction == "India" ? 1 : 0,
                    record.country_of_transaction == "Russia" ? 1 : 0,
                    record.country_of_transaction == "USA" ? 1 : 0,
                    record.country_of_transaction == "UnitedKingdom" ? 1 : 0,

                    (record.shipping_address ?? record.country_of_transaction) == "India" ? 1 : 0,
                    (record.shipping_address ?? record.country_of_transaction) == "Russia" ? 1 : 0,
                    (record.shipping_address ?? record.country_of_transaction) == "USA" ? 1 : 0,
                    (record.shipping_address ?? record.country_of_transaction) == "UnitedKingdom" ? 1 : 0,

                    record.bank == "HSBC" ? 1 : 0,
                    record.bank == "Halifax" ? 1 : 0,
                    record.bank == "Lloyds" ? 1 : 0,
                    record.bank == "Metro" ? 1 : 0,
                    record.bank == "Monzo" ? 1 : 0,
                    record.bank == "RBS" ? 1 : 0,

                    record.type_of_card == "Visa" ? 1 : 0,
                };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };

                string predictionResult;
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
                }

                predictions.Add(new OrderPrediction { Orders = record, Prediction = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel

            }
            return View(predictions);
        }


    }
}