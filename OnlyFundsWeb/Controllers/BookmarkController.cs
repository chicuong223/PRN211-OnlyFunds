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
        public ActionResult AddBookmark(string username, int postId)
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