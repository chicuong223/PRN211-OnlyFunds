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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, int? page)
        {
            if (username != null && password != null)
            {
                Admin user = adminRepository.CheckLogin(username, password);
                if (user != null)
                {
                    TempData["userList"] = userRepository.GetUsers(1);
                    TempData["reportList"] = reportRepository.GetReports();
                    TempData["reportSolved"] = reportRepository.GetReportByStatus(true); 
                    TempData["reportPending"] = reportRepository.GetReportByStatus(false);
                    HttpContext.Session.SetString("user", username);
                    ViewBag.User = HttpContext.Session.GetString("user");
                    IEnumerable<PostReport> reportList = reportRepository.GetReports();
                   
                    return View("Success", reportList);
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

        public IActionResult UserList(int? page)
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
        }

        /*
        public IActionResult ReportSolved(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            int pageSize = 6;
            int count = context.Users.Count();
            int end = count / pageSize;
            IEnumerable<PostReport> reportSolved = context.PostReports.Where(r => r.IsSolved).ToList();
            ViewBag.end = end;
            return View("Success", reportSolved);
        }

        public IActionResult ReportPending(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            int pageSize = 6;
            int count = context.Users.Count();
            int end = count / pageSize;
            IEnumerable<PostReport> reportPending = context.PostReports.Where(r => !r.IsSolved).ToList();
            ViewBag.end = end;
            return View("Success", reportPending);
        }

        public IActionResult TotalReport(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            int pageSize = 6;
            int count = context.Users.Count();
            int end = count / pageSize;
            IEnumerable<PostReport> reports = reportRepository.GetReports();
            ViewBag.end = end;
            return View("Success", reports);
        }*/

    }
}
