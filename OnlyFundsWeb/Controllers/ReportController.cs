using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace OnlyFundsWeb.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepository reportRepo = new ReportRepository();
        private IPostRepository postRepo = new PostRepository();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int? postid, string desc)
        {
            if (postid == null)
                return NotFound();
            Post post = postRepo.GetPostById(postid.Value);
            if (post == null)
                return NotFound();
            PostReport report = new PostReport
            {
                ReportId = reportRepo.GetMaxReportId() + 1,
                ReportDescription = desc,
                PostId = postid.Value,
                ReporterUsername = HttpContext.Session.GetString("user"),
                ReportDate = DateTime.Now,
                IsSolved = false
            };
            reportRepo.AddReport(report);
            Console.WriteLine(report.IsSolved);
            return RedirectToAction("Details", "Posts", new { id = postid.Value });
        }
    }
}
