using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Assignment3.Code.DataModels;
using Assignment3.Code.Repositories;

namespace Assignment3.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly IDataEntityRepository<BlogPost> _repository;

        public BlogController(IConfiguration configuration)
        {
            _repository = new BlogDBRepository(configuration);
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<BlogPost> blogPosts = _repository.GetList() ?? new List<BlogPost>();
            return View("/Views/Home/Index.cshtml", blogPosts);
        }

        [Route("Add")]
        public IActionResult Add()
        {
            return View(new BlogPostModel());
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                BlogPost blogPost = new BlogPost
                {
                    Author = model.Author,
                    Title = model.Title,
                    Content = model.Content,
                    Timestamp = model.Timestamp
                };
                _repository.Save(blogPost);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            BlogPost blogPost = _repository.Get(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            BlogPostModel model = new BlogPostModel
            {
                ID = blogPost.ID,
                Author = blogPost.Author,
                Title = blogPost.Title,
                Content = blogPost.Content,
                Timestamp = blogPost.Timestamp
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                BlogPost blogPost = new BlogPost
                {
                    ID = model.ID,
                    Author = model.Author,
                    Title = model.Title,
                    Content = model.Content,
                    Timestamp = model.Timestamp
                };
                _repository.Save(blogPost);
                return RedirectToAction("Index");
            }
            return View(model);
        }


    }
}
