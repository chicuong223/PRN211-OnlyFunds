﻿using BusinessObjects;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Success(int?  page, string searchString)
        {
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Post> postList = postRepository.GetAllPost(page.Value);
            int pageSize = 3;
            int count = postRepository.CountAllPost();
            int end = count / pageSize;
            if (count % 3 != 0)
            {
                end = end + 1;
            }

            ViewBag.end = end;
            ViewBag["PostList"] = postList;

            return View("Success");
        }

        public ActionResult ChangePassword()
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
                    userRepository.AddUser(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(user);
            }
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
