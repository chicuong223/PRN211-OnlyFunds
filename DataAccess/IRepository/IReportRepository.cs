using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IReportRepository
    {
        void AddReport(PostReport report);
        void SolveReport(PostReport report);
        PostReport GetReportById(int reportId);
        IEnumerable<PostReport> GetReports();
        IEnumerable<PostReport> GetReportByStatus(bool status);
        int GetMaxReportId();
        IEnumerable<PostReport> GetReportsByPost(int postId);
    }
}
