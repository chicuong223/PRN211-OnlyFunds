using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace OnlyFundsWeb.Controllers
{
    public class PostLikeController : Controller
    {
        private IPostLikeRepository postLikeRepository = null;
        public PostLikeController()
        {
            postLikeRepository = new PostLikeRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLike(int postId, string username)
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Index", "User");
            }

            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            try
            {
                PostLike like = new PostLike();
                postLikeRepository.CountPostLike(postId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }
    }
}
