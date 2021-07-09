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
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace OnlyFundsWeb.Controllers
{
    public class UserController : Controller
    {
        IUserRepository userRepository= new UserRepository();
        IPostRepository postRepository = new PostRepository();
        IBookmarkRepository bookmarkRepository = new BookmarkRepository();
        public ActionResult Success(int? page, string searchString)
        {
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
            int pageSize = 3;
            int count = postRepository.CountAllPost();
            int end = count / pageSize;
            if (count % 3 != 0)
            {
                end = end + 1;
            }

            User user = userRepository.GetUserByName(username);
            ViewBag.User = user;
            ViewBag.end = end;
            return View("Success", postList);
        }
        public ActionResult BookmarkedPosts(int? page)
        {
            string username = HttpContext.Session.GetString("user");
            if (username == null)
            {
                return RedirectToAction("Index");
            }
            if (page == null)
            {
                page = 1;
            }
            var postList = bookmarkRepository.GetPostsByBookmark( username, page.Value);
            int pageSize = 3;
            int count = postRepository.CountAllPost();
            int end = count / pageSize;
            if (count % 3 != 0)
            {
                end = end + 1;
            }

            User user = userRepository.GetUserByName(username);
            ViewBag.User = user;
            ViewBag.end = end;
            return View(postList);
        }

        public ActionResult ChangePassword()
        {

            return View("PasswordChange");
        }

        [HttpPost]
        public ActionResult ChangePassword(string username, string newPassword)
        {

            return View("PasswordChange");
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
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmOTP(string otp)
        {
            if (TempData["Attempts"] == null)
            {
                TempData["Attempts"] = 3;
            }
            int attempt = int.Parse(TempData["Attempts"].ToString());
            if (attempt == 0)
            {
                TempData["Attempts"] = null;
                TempData["otp"] = null;
                TempData["newAccount"] = null;
                ViewBag.Message = "You ran out of attempts<br>Make sure to use your own email";
                return View(nameof(Register));
            }
            Object jsonNewUser = TempData.Peek("newAccount");
            User newUser = jsonNewUser == null ? null : JsonSerializer.Deserialize<User>((string)jsonNewUser);
            if (otp != null && otp.Equals(TempData["otp"].ToString()))
            {
                userRepository.AddUser(newUser);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = $"Sit back and & Relax! While we verify your Email address: {newUser.Email}";
            ViewBag.Attempts = $"{attempt} attempts left";
            attempt--;
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
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserInfo(string username, User user)
        {
            if (!username.Equals(user.Username))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                userRepository.UpdateUser(user);
                return RedirectToAction(nameof(DetailsUser));
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
