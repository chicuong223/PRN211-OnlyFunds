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
    public class CommentLikeController : Controller
    {
        private ICommentLikeRepository commentLikeRepository = null;

        public CommentLikeController()
        {
            commentLikeRepository = new CommentLikeRepository();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void AddCommentLike(string username, int commentId, int postId)
        {
            if (HttpContext.Session.GetString("user") == null) 
            {
                /*return RedirectToAction("Index", "User");*/
            }

            try
            {
                CommentLike _like = commentLikeRepository.CheckCommentLike(username, commentId);
                if (_like == null)
                {
                    CommentLike like = new CommentLike();
                    like.Username = username;
                    like.CommentId = commentId;
                    commentLikeRepository.AddCommentLike(like);
                    /*return RedirectToAction("Details", "Posts", new {id = postId});*/
                }
                else
                {
                    commentLikeRepository.DeleteLike(username, commentId);
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
