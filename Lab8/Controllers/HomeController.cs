using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab8.Models;
using Lab8.Repositories;
using Microsoft.AspNetCore.OutputCaching;

namespace Lab8.Controllers;
[Route("")]
[Route("Home")]
public class HomeController : Controller
{
    private readonly IImageRepository _ImageRepo;

    public HomeController(IImageRepository imageRepo)
    {
        _ImageRepo = imageRepo;
    }
    [Route("")]
    [HttpGet("Index")]
    [OutputCache(Duration = 300)]
    public IActionResult Index()
    {
        var images = _ImageRepo.GetImages();
        return View(images);
    }
    [HttpGet("AddImage")]
    public IActionResult AddImage()
    {
        return View();
    }
    [HttpPost("AddImage")]
    public IActionResult AddImage(ImageModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        using var memoryStream = new MemoryStream();
        model.File.CopyTo(memoryStream);
        var image = new DataObjects.ImageObject
        {
            FileName = model.File.FileName,
            FileData = memoryStream.ToArray(),
            Description = model.Description
        };

        _ImageRepo.SaveImage(image);
        return RedirectToAction("Index");
    }
    [HttpGet("Image/{id}")]
    [ResponseCache(Duration = 1800, Location = ResponseCacheLocation.Client, NoStore = false)]
    public IActionResult GetImage(int id)
    {
        var imageData = _ImageRepo.GetImageData(id);
        if (imageData == null || imageData.Length == 0)
        {
            return NotFound();
        }
        var mimeType = "image/png";
        return File(imageData, mimeType);
    }
    
}
