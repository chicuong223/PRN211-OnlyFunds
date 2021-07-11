using BusinessObjects;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlyFundsWeb.Helpers;

namespace OnlyFundsWeb.Controllers
{
    public class UserController : Controller
    {
        private IWebHostEnvironment env;
        IUserRepository userRepository= null;
        IPostRepository postRepository = null;
        private ICategoryRepository categoryRepository = null;
        public UserController(IWebHostEnvironment env)
        {
            this.env = env;
            userRepository = new UserRepository();
            postRepository = new PostRepository();
            categoryRepository = new CategoryRepository();
        }
        public ActionResult Success(int?  page, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                return RedirectToAction("Index");
            }

            if (page == null)
            {
                page = 1;
            }
            var postList = postRepository.GetAllPost(page.Value);
            int count = postRepository.CountAllPost();
            if (!String.IsNullOrEmpty(searchString))
            {
                postList = postRepository.SearchPostsByTitle(searchString, page.Value);
                count = postRepository.CountSearchPost(searchString);
            }
            int pageSize = 3;
            
            /*int count = postList.Count();*/
            int end = count / pageSize;
            if (count % 3 != 0)
            {
                end = end + 1;
            }
           
            User user = userRepository.GetUserByName(username);
            IEnumerable<Category> categoryList = categoryRepository.GetCategories();
            string categoryString = JsonSerializer.Serialize(categoryList);
            HttpContext.Session.SetString("CategoryList", categoryString);
            ViewBag.User = user;
            ViewBag.end = end;
            return View("Success",postList);
        }
        public ActionResult ChangePassword(string username)
        {
            User user = userRepository.GetUserByName(username);

            return View("PasswordChange", user);
        }

        [HttpPost]
        public ActionResult ChangePassword(string username, string newPassword)
        {
            if (username == null)
            {
                return NotFound();
            }

            User user = userRepository.GetUserByName(username);
            userRepository.ChangePassword(user, newPassword);
            return RedirectToAction("Success");
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var userList = userRepository.GetUsers(1);
            return View(userList);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if(username != null && password != null)
            {
                User user = userRepository.CheckLogin(username, password);
                if(user != null)
                {
                    string userStr = JsonSerializer.Serialize(user);
                    HttpContext.Session.SetString("userLayout", userStr);
                    HttpContext.Session.SetString("user", username);
                    
                    ViewBag.User = HttpContext.Session.GetString("user");
                    return RedirectToAction("Success");
                }
                else
                {
                    ViewBag.Message = "Incorrect Username or Password";
                    return View("Index");
                }
            }
            ViewBag.Message = "Error";
            return View("Index");
        }

        // GET: UserController/Details/5
        public ActionResult DetailsUser(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var user = userRepository.GetUserByName(username);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, IFormFile AvatarUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User existed = userRepository.GetUserByEmail(user.Email);
                    if (existed != null)
                    {
                        ViewBag.Message = "Email existed";
                        return View();
                    }
                    existed = userRepository.GetUserByName(user.Username);
                    if (existed != null)
                    {
                        ViewBag.Message = "Username existed";
                        return View();
                    }

                    if (AvatarUrl == null)
                    {
                        user.AvatarUrl = "Avatar.png";
                    }
                    else
                    {
                        user.AvatarUrl = Utilities.UploadAvatar(AvatarUrl, env, user.Username);

                    }
                    TempData["newAccount"] = JsonSerializer.Serialize(user);
                }
                return RedirectToAction(nameof(ConfirmOTP));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(user);
            }
        }
        // GET: UserController/Create
        public ActionResult ConfirmOTP()
        {
            Object jsonNewUser = TempData.Peek("newAccount");
            User newUser = jsonNewUser == null ? null : JsonSerializer.Deserialize<User>((string)jsonNewUser);
            string otp = new Random().Next(999999).ToString("D6");

            EmailSender emailSender = new EmailSender();
            string subject = "OTP";
            string body = $"{otp} is your Funds on verification code.";
            TempData["otp"] = otp;
            emailSender.sendEmail(subject, body, newUser.Email);
            ViewBag.Message = $"Sit back and & Relax! While we verify your Email address: {newUser.Email}";
            //-----
            TempData["Attempts"] = 4;
            //----------
            return View();
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public ActionResult ConfirmOTP(string otp)
        {

            /*if (TempData["Attempts"] == null)
            {
                TempData["Attempts"] = 3;
            }*/
            //--------

            //-----------
            int attempt = int.Parse(TempData["Attempts"].ToString());

            if (attempt == 0)
            {
                object jsonUser = TempData.Peek("newAccount");
                BusinessObjects.User currentUser = JsonSerializer.Deserialize<User>(jsonUser.ToString());
                Utilities.DeleteFile(currentUser.AvatarUrl, env, "images");
                TempData["Attempts"] = null;
                TempData["otp"] = null;
                TempData["newAccount"] = null;
                ViewBag.Message = "You ran out of attempts<br>Make sure to use your own email";
                return View(nameof(Register));
            }
            Object jsonNewUser = TempData.Peek("newAccount");
            User newUser = jsonNewUser == null ? null : JsonSerializer.Deserialize<User>((string)jsonNewUser);
            if (otp != null && otp.Equals(TempData.Peek("otp").ToString()))
            {
                userRepository.AddUser(newUser);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = $"Sit back and & Relax! While we verify your Email address: {newUser.Email}";
            attempt--;
            ViewBag.Attempts = $"{attempt} attempts left";
            TempData["Attempts"] = attempt;
            return View();
        }


        // GET: UserController/Edit/5
        public ActionResult UpdateUserInfo(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var user = userRepository.GetUserByName(username);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserInfo(string username, User user, IFormFile AvatarUrl)
        {
            if (!username.Equals(user.Username))
            {
                return NotFound();
            }

            User oldUser = userRepository.GetUserByName(username);
            if (ModelState.IsValid)
            {
                if (AvatarUrl != null)
                {
                    if (!oldUser.AvatarUrl.Equals("Avatar.png"))
                    {
                        Utilities.DeleteFile(oldUser.AvatarUrl, env, "images");
                    }
                    user.AvatarUrl= Utilities.UploadAvatar(AvatarUrl, env, user.Username);
                }
                else
                {
                    user.AvatarUrl = oldUser.AvatarUrl;
                }
                userRepository.UpdateUser(user);
                return RedirectToAction(nameof(DetailsUser), new {username = username});
            }
            return View(user);
        }

        // GET: UserController/Delete/5
        public ActionResult DeleteUser(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id, IFormCollection collection)
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
        public ActionResult Logout()
        {
            var session = HttpContext.Session;
            if (session.GetString("user") != null)
            {
                session.Remove("user");
            }
            return RedirectToAction("Index", "Home");
        }

         
    }
}
