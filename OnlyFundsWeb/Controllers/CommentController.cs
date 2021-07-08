using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsWeb.Controllers
{
    public class CommentController : Controller
    {
        private ICommentRepository cmtRepo = new CommentRepository();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(int postid, string content)
        {
            if(HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Index", "User");
            }
            try
            {
                Comment cmt = new Comment();
                cmt.CommentId = cmtRepo.GetMaxCommentId() + 1;
                cmt.PostId = postid;
                cmt.CommentDate = DateTime.Now;
                cmt.Content = content;
                cmt.Username = HttpContext.Session.GetString("user");
                cmtRepo.AddComment(cmt);
                return RedirectToAction("Details", "Posts", new { id = postid });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, string newContent, int postid)
        {
            if (string.IsNullOrWhiteSpace(newContent))
            {
                return RedirectToAction("Details", "Posts", new { id = postid });
            }
            try
            {
                Comment cmt = cmtRepo.GetComment(id);
                if (cmt == null)
                    throw new Exception("Comment not found");
                cmt.Content = newContent;
                cmtRepo.EditComment(cmt);
                return RedirectToAction("Details", "Posts", new { id = postid });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Delete(int id, int postid)
        {
            if (HttpContext.Session.GetString("user") == null)
                return RedirectToAction("Index", "User");
            try
            {
                Comment cmt = cmtRepo.GetComment(id);
                if (cmt == null)
                    throw new Exception("Comment not found");
                cmtRepo.DeleteComment(cmt);
                return RedirectToAction("Details", "Posts", new { id = postid });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("Error");
            }
        }
    }
}
