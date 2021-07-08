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
using OnlyFundsWeb.Helpers;
using System.Linq;

namespace OnlyFundsWeb.Controllers
{
    public class PostsController : Controller
    {
        IWebHostEnvironment env;
        private IPostRepository postRepository = new PostRepository();
        private IUserRepository userRepository = new UserRepository();
        private ICategoryRepository categoryRepository = new CategoryRepository();
        private IPostCategoryMapRepository postCategoryMapRepository = new PostCategoryMapRepository();
        private ICommentRepository cmtRepository = new CommentRepository();
        private PRN211_OnlyFunds_CopyContext context = new PRN211_OnlyFunds_CopyContext();
        public PostsController(IWebHostEnvironment env) => this.env = env;

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
                IEnumerable<Comment> cmt = cmtRepository.GetCommentsByPost(post.PostId);
                ViewBag.Comments = cmt;
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
        public ActionResult Create(IFormFile file, Post post, int[] category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(post.PostTitle))
                    throw new Exception("Title is required");
                if (string.IsNullOrWhiteSpace(post.PostDescription))
                    throw new Exception("Description is required");
                string fileName = Utilities.UploadFile(file, env, "postfiles");
                post.PostId = context.Posts.Max(p => p.PostId) + 1;
                post.UploaderUsername = HttpContext.Session.GetString("user");
                post.UploadDate = DateTime.Now;
                post.FileUrl = fileName;
                postRepository.InsertPost(post);
                foreach(int catID in category)
                {
                    PostCategoryMap map = new PostCategoryMap
                    {
                        CategoryId = catID,
                        PostId = post.PostId
                    };
                    postCategoryMapRepository.AddPostMap(map);
                }
                return RedirectToAction(nameof(GetPostByUser), new { username = post.UploaderUsername });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Post post = postRepository.GetPostById(id.Value);
            if (post == null)
                return NotFound();
            return View(post);
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

        public IActionResult Cancel()
        {
            return View("Create");
        }
    }
}
