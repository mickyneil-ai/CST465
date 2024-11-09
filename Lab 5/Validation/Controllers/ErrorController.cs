using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode:int}")]
        [Route("Error")]
        public IActionResult Index(int statusCode)
        {
            // Check the status code and return the appropriate view
            if (statusCode == 404)
            {
                return View("Error404");
            }
            else if (statusCode == 500)
            {
                return View("Error500");
            }
            
            // Fallback for other status codes
            return View(statusCode);
        }
    }
}
