using BusinessObjects;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ReportRepository : IReportRepository
    {
        public void AddReport(PostReport report)
        => ReportDAO.Instance.AddReport(report);

        public int GetMaxReportId() => ReportDAO.Instance.GetMaxReportId();

        public PostReport GetReportById(int reportId)
        => ReportDAO.Instance.GetReportById(reportId);

        public IEnumerable<PostReport> GetReports()
        => ReportDAO.Instance.GetReports();

        public IEnumerable<PostReport> GetReportsByPost(int postId)
        {
            return ReportDAO.Instance.GetReportsByPost(postId);
        }

        public IEnumerable<PostReport> GetReportsByStatus(bool status)
        => ReportDAO.Instance.GetReportByStatus(status);
        public void SolveReport(PostReport report)
        => ReportDAO.Instance.SolveReport(report);
        
    }
}
