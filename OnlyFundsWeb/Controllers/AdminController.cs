using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace OnlyFundsWeb.Controllers
{
    public class AdminController : Controller
    {
        private IReportRepository reportRepository = new ReportRepository();
        private IAdminRepository adminRepository = new AdminRepository();
        private IUserRepository userRepository = new UserRepository();
        private ICategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            /*IEnumerable<Category> categories = categoryRepository.GetCategories();
            ViewBag["categories"] = categories;*/
            IEnumerable<PostReport> reportLIst = reportRepository.GetReports();
            IEnumerable<User> userList = userRepository.GetUsers(1);
            string adminName = HttpContext.Session.GetString("admin");
            Admin admin = adminRepository.GetAdminByUname(adminName);
            ViewBag.userList = userList;
            ViewBag.admin = admin;
            return View("Success", reportLIst);
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username != null && password != null)
            {
                Admin user = adminRepository.CheckLogin(username, password);
                if (user != null)
                {
                    HttpContext.Session.SetString("admin", username);
                    /*ViewBag.User = HttpContext.Session.GetString("admin");*/
                    /*IEnumerable<PostReport> reportList = reportRepository.GetReports();*/
                   
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

        /*public IActionResult UserList(int? page)
        {
            IEnumerable<User> userList = userRepository.GetUsers(page.Value);
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 6;
            int count = userList.Count();
            int end = count / pageSize;
            
            if (count % 6 != 0)
            {
                end = end + 1;
            }
            ViewBag.end = end;
            return View("Success", userList);
        }*/
    }
}
