using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
        private IPostRepository postRepository = null;
        private IUserRepository userRepository = null;
        private ICategoryRepository categoryRepository =null;
        private IPostCategoryMapRepository postCategoryMapRepository = null;
        private ICommentRepository cmtRepository = null;
        private IPostLikeRepository postLikeRepository = null;
        private ICommentLikeRepository commentLikeRepository = null;
        private IBookmarkRepository bookmarkRepository = null;

        public PostsController(IWebHostEnvironment env)
        {
            this.env = env;
            postRepository = new PostRepository();
            userRepository = new UserRepository();
            categoryRepository = new CategoryRepository();
            postCategoryMapRepository = new PostCategoryMapRepository();
            cmtRepository = new CommentRepository();
            postLikeRepository = new PostLikeRepository();
            commentLikeRepository = new CommentLikeRepository();
            bookmarkRepository = new BookmarkRepository();
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
                var postList = postRepository.GetPostByUser(user, page.Value);
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

        public ActionResult GetPostByCategory(int categoryId, int? page)
        {
            try
            {
                if (categoryId == 0)
                {
                    return NotFound();
                }

                Category category = categoryRepository.GetCategoryById(categoryId);
                if (category == null)
                {
                    return NotFound();
                }
                if (page == null)
                {
                    page = 1;
                }
                IEnumerable<Post> postList = postCategoryMapRepository.FilterPostByCategory(categoryId, page.Value);
                ViewBag.Category = category;
                int pageSize = 3;
                int count = postRepository.CountPostByCategory(category);
                int end = count / pageSize;
                if (count % 3 != 0)
                {
                    end = end + 1;
                }

                ViewBag.end = end;
                return View("PostList", postList);
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View("Error");
            }
        }
        // GET: PostsController/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                string username = HttpContext.Session.GetString("user");
                if (id == null)
                {
                    return NotFound();
                }
                Post post = postRepository.GetPostById(id.Value);
                if (post == null)
                    return NotFound();
                //-------------
                IEnumerable<Comment> cmt = cmtRepository.GetCommentsByPost(post.PostId);
                List<User> cmtUsers = new List<User>();
                if (cmt != null)
                {
                    foreach (var comment in cmt)
                    {
                        IEnumerable<BusinessObjects.CommentLike> commentLikeList =
                            commentLikeRepository.GetCommentLikeByCommentId(comment.CommentId);
                        foreach (var commentLike in commentLikeList)
                        {
                            comment.CommentLikes.Add(commentLike);
                        }
                    }
                }
                //-----------------
                int postLike = postLikeRepository.CountPostLike(id.Value);
                int postComment = cmt.Count();
                //-------------
                foreach (Comment c in cmt)
                {
                    User user = userRepository.GetUserByName(c.Username);
                    cmtUsers.Add(user);
                }
                /*if (username != null &&!username.Equals(post.UploaderUsername))
                {
                    
                }*/
                IReportRepository reportRepo = new ReportRepository();
                IEnumerable<PostReport> reports = reportRepo.GetReportsByPost(post.PostId);
                ViewBag.Reports = reports;
                User currentUser = userRepository.GetUserByName(username);
                ViewBag.CurrentUser = currentUser;
                ViewBag.CmtUsers = cmtUsers;
                ViewBag.Comments = cmt;
                ViewBag.PostLike = postLike;
                ViewBag.PostComment = postComment;

                //for Bookmark
                try
                {
                    Bookmark bookmark = bookmarkRepository.GetBookmark(username, id.Value);
                    ViewBag.IsBookmarked = bookmark != null;
                    //ViewBag.IsBookmarked = true;
                }
                catch(Exception ex)
                {
                    //return RedirectToAction("Index", "User");
                }

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
            ViewBag.CategoryList = categoryRepository.GetCategories();
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
                post.PostId = postRepository.GetMaxPostId() +1;
                post.UploaderUsername = HttpContext.Session.GetString("user");
                post.UploadDate = DateTime.Now;
                post.FileUrl = fileName;
                postRepository.InsertPost(post);
                foreach (int catID in category)
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
            string username = HttpContext.Session.GetString("user");
            if (username == null)
                return RedirectToAction("Index", "User");
            if (id == null)
                return NotFound();
            Post post = postRepository.GetPostById(id.Value);
            if (post == null)
                return NotFound();
            ViewBag.CategoryList = categoryRepository.GetCategories();
            if (!username.Equals(post.UploaderUsername))
            {
                ViewBag.Error = "You are not allowed to edit posts of others";
                return View("Error");
            }
            return View(post);
        }
        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post, IFormFile file, int[] cate)
        {
            try
            {
                Post oldPost = postRepository.GetPostById(id);
                if (oldPost == null)
                    throw new Exception("Post not found");
                post.UploaderUsername = oldPost.UploaderUsername;
                post.UploadDate = oldPost.UploadDate;
                if (file == null)
                    post.FileUrl = oldPost.FileUrl;
                else
                {
                    Utilities.DeleteFile(oldPost.FileUrl, env, "postfiles");
                    post.FileUrl = Utilities.UploadFile(file, env, "postfiles");
                }
                IEnumerable<Category> postCatList = categoryRepository.GetCategoriesByPost(post.PostId);
                foreach (var category in postCatList)
                {
                    postCategoryMapRepository.DeletePostMap(post, category);
                }
                foreach (int catID in cate)
                {
                    PostCategoryMap map = new PostCategoryMap
                    {
                        PostId = post.PostId,
                        CategoryId = catID
                    };
                    postCategoryMapRepository.AddPostMap(map);
                }
                postRepository.UpdatePost(post);
                return RedirectToAction(nameof(Details), new { id = post.PostId });
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("Error");
            }
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