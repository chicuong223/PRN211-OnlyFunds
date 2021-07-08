using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ReportDAO
    {
        private static ReportDAO instance = null;
        private static readonly object instanceLock = new object();

        public static ReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }

                    return instance;
                }
            }
        }

        public void AddReport(PostReport report)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                //report.IsSolved = false;
                context.PostReports.Add(report);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void SolveReport(PostReport report)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                report.IsSolved = true;
                context.Entry<PostReport>(report).Property(r => r.IsSolved).IsModified = true;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public PostReport GetReportById(int reportId)
        {
            PostReport report = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                report = context.PostReports.Find(reportId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return report;
        }

        public IEnumerable<PostReport> GetReports()
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                return context.PostReports.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PostReport> GetReportsByPost(int postId)
        {
            List<PostReport> lst = new List<PostReport>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                lst = context.PostReports.Where(r => r.PostId == postId).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return lst;
        }

        public int GetMaxReportId()
        {
            int reportId = 0;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                reportId = context.PostReports.Max(r => r.ReportId);
                if (reportId == 0)
                {
                    throw new Exception("no report");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return reportId;
        }
        public IEnumerable<PostReport> GetReportByStatus(bool status)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                IEnumerable<PostReport> reports = context.PostReports.Where(r => r.IsSolved == status).ToList();
                return reports;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
