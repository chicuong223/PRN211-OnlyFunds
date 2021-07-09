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
    public class BookmarkController : Controller
    {
        IBookmarkRepository bookmarkRepository;
        public BookmarkController()
        {
            bookmarkRepository = new BookmarkRepository();
        }
        //// GET: BookmarkController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: BookmarkController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: BookmarkController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BookmarkController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        
        public ActionResult AddBookmark(string username, int postId )
        {
            bookmarkRepository.AddBookmark(username, postId);
            return RedirectToAction("Details", "Posts", new { id = postId });
        }
        public ActionResult RemoveBookmark(string username, int postId)
        {
            Bookmark bookmark = bookmarkRepository.GetBookmark(username, postId);
            bookmarkRepository.DeleteBookmark(bookmark);
            return RedirectToAction("Details", "Posts", new { id = postId });
        }
    }
}
