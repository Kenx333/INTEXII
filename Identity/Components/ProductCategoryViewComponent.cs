using Microsoft.AspNetCore.Mvc;
using INTEXII.Models;

namespace INTEXII.Components
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private IProductRepository _productRepo;

        //Constructor
        public ProductCategoryViewComponent(IProductRepository temp)
        {
            _productRepo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProjectType = RouteData?.Values["productCategory"];

            var productCategories = _productRepo.Products
                .Select(x => x.category)
                .Distinct()
                .OrderBy(x => x);

            return View(productCategories);
        }
    }
}
