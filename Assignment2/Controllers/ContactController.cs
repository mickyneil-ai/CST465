using Microsoft.AspNetCore.Mvc;
using ASPNETClass.Models;

public class ContactController : Controller
{
    [Route("ContactHTML")]
    public IActionResult ContactHTML()
    {
        ViewBag.Title = "Contact (HTML)";
        return View();
    }

    [Route("ContactTagHelper")]
    public IActionResult ContactTagHelper()
    {
        ViewBag.Title = "Contact (Tag Helper)";
        return View();
    }

    [HttpPost("Contact")]
    public IActionResult Contact(ContactModel model)
    {
        ViewBag.Title = "Contact Submitted";
        return View("ContactResult", model);
    }
}
