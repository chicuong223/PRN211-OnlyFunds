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
        private PRN211_OnlyFunds_CopyContext context = new PRN211_OnlyFunds_CopyContext();
        private IPostRepository postRepository = new PostRepository();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            using var context = new PRN211_OnlyFunds_CopyContext();
            if (page == null)
                page = 1;
            int pageSize = 3;
            int count = context.Posts.Count();
            int end = count / pageSize;
            IEnumerable<Post> postList = postRepository.GetAllPost(page.Value);
            if (count % 3 != 0)
            {
                end = end + 1;
            }
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
