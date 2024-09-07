using Microsoft.AspNetCore.Mvc;

namespace Assignment_02.Controllers
{
    public class ProductsController : Controller
    {
        // Model Binding
        // 1.From Form
        // 2.From Rout
        // 3.Query String
        // 4.From Body
        // 5.From Header



        // Action => Public NonStatic Method
        public IActionResult Get(int id, string name, Product product)
        {
            //ContentResult contentResult = new ContentResult();
            //contentResult.Content = $"Product {id}";
            //contentResult.ContentType = "text/html";
            //contentResult.StatusCode = StatusCodes.Status200OK;


            return Content($"Product {id} : {name}", "text/html");
        }

        public IActionResult Redirect()
        {
            //RedirectResult redirectResult = new RedirectResult("https://www.google.com");
            return Redirect("https://www.google.com");
        }

        public IActionResult RedirectToAction()
        {
            //RedirectToActionResult redirectResult = new RedirectToActionResult("Get", "Products", new { id = 10 });
            
            return RedirectToAction(nameof(Get), new { id = 10});
        }
    }
}
