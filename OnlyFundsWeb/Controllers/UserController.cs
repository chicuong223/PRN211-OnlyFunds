using BusinessObjects;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.IRepository;
using DataAccess.Repository;
using System.Text.Json;

namespace OnlyFundsWeb.Controllers
{
    public class UserController : Controller
    {
        IUserRepository userRepository = new UserRepository();

        private PRN211_OnlyFunds_CopyContext context = new PRN211_OnlyFunds_CopyContext();

        public ActionResult Success()
        {
            return View("Success");
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
            if (username != null && password != null)
            {
                User user = userRepository.CheckLogin(username, password);
                if (user != null)
                {
                    HttpContext.Session.SetString("user", username);
                    ViewBag.User = HttpContext.Session.GetString("user");
                    return View("Success");
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

            var user = context.Users.FirstOrDefault(u => u.Username.Equals(username));
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
            if (ModelState.IsValid)
            {
                //context.Users.Add(user);
                //context.SaveChanges();
                //return RedirectToAction(nameof(Index));

                TempData["newAccount"] = JsonSerializer.Serialize(user);
                return RedirectToAction(nameof(ConfirmOTP));
            }
            return View(user);
        }
        public ActionResult ConfirmOTP()
        {
            Object jsonNewUser = TempData.Peek("newAccount");
            User newUser = jsonNewUser == null ? null : JsonSerializer.Deserialize<User>((string)jsonNewUser);
            string otp = new Random().Next(999999).ToString("D6");

            EmailSender emailSender = new EmailSender();
            string subject = "OTP";
            string body = $"Your otp is {otp}";
            TempData["otp"] = otp;
            emailSender.sendEmail(subject, body, newUser.Email);
            ViewBag.Message = $"OTP code is sent to {newUser.Email}";
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmOTP(string otp)
        {
            if(TempData["Attempts"]==null)
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
            if (otp!=null && otp.Equals(TempData["otp"].ToString()))
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = $"OTP code is sent to {newUser.Email}";
            ViewBag.Attempts=$"{attempt} attempts left";
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

            var user = context.Users.Find(username);
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
                context.Update(user);
                context.SaveChanges();
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
