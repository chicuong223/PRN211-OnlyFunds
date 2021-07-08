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

        public PostReport GetReportById(int reportId)
        => ReportDAO.Instance.GetReportById(reportId);

        public IEnumerable<PostReport> GetReports()
        => ReportDAO.Instance.GetReports();

        public void SolveReport(PostReport report)
        => ReportDAO.Instance.SolveReport(report);

        public IEnumerable<PostReport> GetReportByStatus(bool status) => ReportDAO.Instance.GetReportByStatus(status);
        public int GetMaxReportId() => ReportDAO.Instance.GetMaxReportId();
    }
}
