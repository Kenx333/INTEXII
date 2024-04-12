using INTEXII.Models;
using INTEXII.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;

namespace INTEXII.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private IProductRepository _productRepository;
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;
        public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash, IProductRepository productRepository, IHostEnvironment hostEnvironment)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
            _productRepository = productRepository;

            _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "fraud_model.onnx");

            //initialize the InferenceSession
            _session = new InferenceSession(_onnxModelPath);
        }

        public IActionResult AdminView()
        {
            return View();  
        }

        public IActionResult AdminHome()
        {
            //var firstThreeProducts = db.Products.Take(3).ToList();

            //// Pass the products to the view
            //return View(firstThreeProducts);

            return View();
        }

        public IActionResult ProductList (int pageNum, string? categoryType)
        {
            int pageSize = 6;
            var p = new ProductsListViewModel
            {
                Products = _productRepository.Products
                .OrderBy(x => x.product_ID)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = categoryType == null ? _productRepository.Products.Count() : _productRepository.Products.Where(x => x.category == categoryType).Count()
                },
            };

            return View(p);
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            var form = _productRepository.Orders
                .OrderBy(x => x.transaction_ID).ToList().Take(100);

            return View(form);
        }
        public IActionResult CustomerList()
        {
            var form = _productRepository.Customers
                .OrderBy(x => x.customer_ID).ToList().Take(100);
            return View(form);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var product = _productRepository.Products
                .OrderBy(x => x.product_ID)
                .ToList();

            return View("AddProduct", new Models.Product());
        }

        [HttpPost]
        public IActionResult AddProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(newProduct);


                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction("ProductList");
            }
            else
            //invalid - return the form with the data the user entered
            {
                var product = _productRepository.Products.OrderBy(x => x.product_ID).ToList();
                return View(newProduct);
            }

        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var updateRecord = _productRepository.Products
                .Single(x => x.product_ID == id);

            return View("AddProduct", updateRecord);
        }

        [HttpPost]
        public IActionResult EditProduct(Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                _productRepository.UpdateProduct(updatedProduct);
                return RedirectToAction("ProductList");
            }
            else
            //invalid - return the form with the data the user entered

            {
                var product = _productRepository.Products
                    .OrderBy(x => x.product_ID)
                    .ToList();
                return View("AddProduct", updatedProduct);
            }
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var taskToDelete = _productRepository.Products
                .Single(x => x.product_ID == id);
            return View("DeleteProduct",taskToDelete);
        }

        [HttpPost]
        public IActionResult DeleteProduct(Models.Product taskToDelete)
        {
            _productRepository.RemoveProduct(taskToDelete);

            return RedirectToAction("ProductList");
        }



        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    //TwoFactorEnabled = true
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                
                // uncomment for email confirmation (link - https://www.yogihosting.com/aspnet-core-identity-email-confirmation/)
                /*if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("Index");
                    else
                    {
                        // log email failed 
                    }
                }*/
                
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }

        public IActionResult ReviewOrders()
        {
            var records = _productRepository.Orders
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
