using Cart_Application_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cart_Application_MVC.Controllers
{
    public class ProductController : Controller
    {
        ProductModel model = new ProductModel();
        CategoryModel categoryModel = new CategoryModel();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Product_List()
        {
            List<ProductModel> products = model.GetProducts();
            return View(products);
        }
        public IActionResult Add_Product()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add_Product(ProductModel prod)
        {
            bool success;
            if(ModelState.IsValid)
            {
                success=model.Insert(prod);
                if(success)
                {
                    TempData["msg"] = "Add Product Successfully...";
                }
                else
                {
                    TempData["msg"] = "Product Not Add ...";
                }
            }
            return View();
        }
        public IActionResult Category_List()
        {
            List<CategoryModel> categories = categoryModel.GetCategory();
            return View(categories);
        }

        public IActionResult Add_Category()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add_Category(CategoryModel category)
        {
            bool res;
            if(ModelState.IsValid)
            {
                res=categoryModel.InsertCategory(category); 
                if(res)
                {
                    TempData["msg"] = "Category Add ...";
                }
                else
                {
                    TempData["msg"] = "Category Not Add ...";
                }
            }
            return View();
        }
    }
}
