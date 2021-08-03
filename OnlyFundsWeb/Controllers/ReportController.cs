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
        private ICommentRepository cmtRepo = new CommentRepository();

        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("Error");
            PostReport report = reportRepo.GetReportById(id.Value);
            if (report == null)
                return View("Error");
            ViewBag.Post = postRepo.GetPostById(report.PostId);
            return View(report);
        }

        public IActionResult WarnUser(int id)
        {
            EmailSender emailSender = new EmailSender();
            PostReport report = reportRepo.GetReportById(id);
            Post reportedPost = postRepo.GetPostById(report.PostId);
            User reportedUser = new UserRepository().GetUserByName(reportedPost.UploaderUsername);
            string subject = "Post Report Warning";
            string body = $"Your post has been reported: \n" +
                $"Post title: {reportedPost.PostTitle}";
            emailSender.sendEmail(subject, body, reportedUser.Email);
            IEnumerable<PostReport> reports = reportRepo.GetReportsByPost(reportedPost.PostId);
            foreach (var r in reports)
            {
                r.IsSolved = true;
                reportRepo.SolveReport(r);
            }
            return RedirectToAction("Success", "Admin");
        }

        public IActionResult DeletePost(int id)
        {
            PostReport report = reportRepo.GetReportById(id);
            Post reportedPost = postRepo.GetPostById(report.PostId);
            IEnumerable<Comment> cmtList = cmtRepo.GetCommentsByPost(reportedPost.PostId);
            if (cmtList.Count() > 0)
            {
                foreach (var cmt in cmtList)
                    cmtRepo.DeleteComment(cmt);
            }
            postRepo.DeletePost(reportedPost.PostId);
            return RedirectToAction("Success", "Admin");
        }

        [HttpPost]
        public IActionResult Solve(int? reportId, string action)
        {
            if (reportId == null)
                return View("Error");
            PostReport report = reportRepo.GetReportById(reportId.Value);
            if (report == null)
                return View("Error");
            if (action.Equals("del-post"))
                return RedirectToAction(nameof(DeletePost), new { id = report.ReportId });
            if (action.Equals("warn-user"))
                return RedirectToAction(nameof(WarnUser), new { id = report.ReportId });
            return RedirectToAction(nameof(Ignore), new { id = report.ReportId });
        }

        public IActionResult Ignore(int id)
        {
            PostReport report = reportRepo.GetReportById(id);
            Post reportedPost = postRepo.GetPostById(report.PostId);
            IEnumerable<PostReport> reports = reportRepo.GetReportsByPost(reportedPost.PostId);
            foreach (var r in reports)
            {
                r.IsSolved = true;
                reportRepo.SolveReport(r);
            }
            return RedirectToAction("Success", "Admin");
        }

        [HttpPost]
        public IActionResult Add(int? postid, string desc)
        {
            if (postid == null)
                return View("Error");
            Post post = postRepo.GetPostById(postid.Value);
            if (post == null)
                return View("Error");
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
            return RedirectToAction("Details", "Posts", new { id = postid });
        }
    }
}