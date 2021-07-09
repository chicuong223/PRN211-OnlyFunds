using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using DataAccess;

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

        public IActionResult WarnUser(PostReport report)
        {
            EmailSender emailSender = new EmailSender();
            Post reportedPost = postRepo.GetPostById(report.PostId.Value);
            User reportedUser = new UserRepository().GetUserByName(reportedPost.UploaderUsername);
            string subject = "Post Report Warning";
            string body = $"Your post has been reported: \n" +
                $"Post title: {reportedPost.PostTitle}";
            emailSender.sendEmail(subject, body, reportedUser.Email);
            IEnumerable<PostReport> reports = reportRepo.GetReportsByPost(reportedPost.PostId);
            foreach(var r in reports)
            {
                reportRepo.SolveReport(r);
            }
            return View();
        }

        public IActionResult DeletePost(PostReport report)
        {
            Post reportedPost = postRepo.GetPostById(report.PostId.Value);
            postRepo.DeletePost(reportedPost.PostId);
            return View();
        }

        public IActionResult Solve(int? reportId, string solveAction)
        {
            if (reportId == null)
                return NotFound();
            PostReport report = reportRepo.GetReportById(reportId.Value);
            if (report == null)
                return NotFound();
            if (solveAction.Equals("Delete"))
                return RedirectToAction(nameof(DeletePost), new { report = report });
            if (solveAction.Equals("Warning"))
                return RedirectToAction(nameof(WarnUser), new { report = report });
            return RedirectToAction(nameof(Ignore), new { report = report });
        }

        public IActionResult Ignore(PostReport report)
        {
            Post reportedPost = postRepo.GetPostById(report.PostId.Value);
            IEnumerable<PostReport> reports = reportRepo.GetReportsByPost(reportedPost.PostId);
            foreach (var r in reports)
            {
                reportRepo.SolveReport(r);
            }
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
