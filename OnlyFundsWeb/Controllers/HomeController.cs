using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlyFundsWeb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;

namespace OnlyFundsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPostRepository postRepository = null;
        private ICategoryRepository categoryRepository = null;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            postRepository = new PostRepository();
            categoryRepository = new CategoryRepository();
        }

        public IActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 3;
            IEnumerable<Post> postList = postRepository.GetAllPost(page.Value);
            int count = postRepository.CountAllPost();
            int end = count / pageSize;
            if (count % 3 != 0)
            {
                end = end + 1;
            }

            ViewBag.CategoryList = categoryRepository.GetCategories();
            ViewBag.end = end;
            return View(postList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
