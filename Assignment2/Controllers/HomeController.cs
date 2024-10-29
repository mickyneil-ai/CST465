using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("")]
    public IActionResult Index()
    {
        ViewBag.Title = "Home";
        return View();
    }
}
