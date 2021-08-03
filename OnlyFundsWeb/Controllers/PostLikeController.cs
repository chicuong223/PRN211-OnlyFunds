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
        private IPostRepository postRepository = null;
        public PostLikeController()
        {
            postLikeRepository = new PostLikeRepository();
            postRepository = new PostRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        public void AddLike(string username, int postId)
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                /*return RedirectToAction("Index", "User");*/
            }
            try
            {
                PostLike _like = postLikeRepository.CheckUserLike(username, postId);
                ViewBag.CheckPostLiked = _like;
                if (_like == null)
                {
                    PostLike like = new PostLike();
                    like.Username = username;
                    like.PostId = postId;
                    postLikeRepository.AddPostLike(like);
                    /*return RedirectToAction("Details", "Posts", new { id = postId });*/
                }
                else
                {
                    postLikeRepository.DeleteLike(username, postId);
                    /*return RedirectToAction("Details", "Posts", new {id = postId});*/
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                /*return View("Error");*/
            }
        }
    }
}
