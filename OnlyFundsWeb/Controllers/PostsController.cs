using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using DataAccess.IRepository;
using DataAccess.Repository;

namespace OnlyFundsWeb.Controllers
{
    public class PostsController : Controller
    {
        IWebHostEnvironment webHostEnvironment;
        private IPostRepository postRepository = new PostRepository();
        private IUserRepository userRepository = new UserRepository();
        public PostsController(IWebHostEnvironment env)
        {
            this.webHostEnvironment = env;
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
        public ActionResult PostList()
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

                User user = userRepository.GetUserByName(username);
                if (user == null)
                {
                    return NotFound();
                }
                if (page == null)
                    page = 1;
                IEnumerable<Post> postList = postRepository.GetPostByUser(user, page.Value);
                int pageSize = 3;
                int count = postRepository.CountPostByUser(user);
                int end = count / pageSize;
                if (count % 3 != 0)
                {
                    end = end + 1;
                }
                ViewBag.end = end;
                ViewBag.user = user.Username;
                return View("PostList", postList);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("Error");
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
                Post post = postRepository.GetPostById(id.Value);
                if (post == null)
                    return NotFound();
                return View(post);
            }
            catch
            {
                return RedirectToAction(nameof(PostList));
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
                postRepository.InsertPost(post);
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
                return RedirectToAction(nameof(PostList));
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
            var post = postRepository.GetPostById(id.Value);
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
                postRepository.DeletePost(id);
                return RedirectToAction(nameof(PostList), new { username = HttpContext.Session.GetString("user") });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
