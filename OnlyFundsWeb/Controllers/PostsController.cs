using BusinessObjects;
using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFundsWeb.Controllers
{
    public class PostsController : Controller
    {
        IWebHostEnvironment webHostEnvironment;
        public readonly PostDAO postDAO;
        public PostsController(IWebHostEnvironment env)
        {
            this.webHostEnvironment = env;
            postDAO = new PostDAO();
        }
        private string UploadFile(IFormFile file)
        {
            if (file == null || file.Length < 0)
            {
                return "";
            }
            string wwwPath = webHostEnvironment.WebRootPath;
            string path = Path.Combine(wwwPath, "postfiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        // GET: PostsController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPostByUser(string username, int? page)
        {
            try
            {
                if (username == null)
                {
                    return NotFound();
                }
                UserDAO userDAO = new UserDAO();
                User user = userDAO.GetUserByUsername(username);
                if (user == null)
                {
                    return NotFound();
                }
                if (page == null)
                    page = 1;
                List<Post> postList = postDAO.GetPostsByUser(user, page.Value);
                int pageSize = 3;
                int count = postDAO.CountPostsByUser(user);
                int end = count / pageSize;
                if (count % 3 != 0)
                {
                    end = end + 1;
                }
                ViewBag.end = end;
                ViewBag.user = user.Username;
                return View("Index", postList);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                Post post = postDAO.GetPostByID(id.Value);
                if (post == null)
                    return NotFound();
                return View(post);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormFile file, Post post)
        {
            try
            {
                string fileName = UploadFile(file);
                post.UploaderUsername = HttpContext.Session.GetString("user");
                post.UploadDate = DateTime.Now;
                post.FileUrl = fileName;
                postDAO.InsertPost(post);
                return RedirectToAction(nameof(GetPostByUser), new { username = post.UploaderUsername });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var post = postDAO.GetPostByID(id.Value);
            if (post == null) return NotFound();
            return View(post);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                postDAO.DeletePost(id);
                return RedirectToAction(nameof(Index), new { username = HttpContext.Session.GetString("user") });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
