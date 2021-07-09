using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects
{
    public partial class PostReport
    {
        public int ReportId { get; set; }
        public string ReporterUsername { get; set; }
        public int PostId { get; set; }
        public string ReportDescription { get; set; }
        public DateTime? ReportDate { get; set; }
        public bool IsSolved { get; set; }

        public virtual Post Post { get; set; }

        public override string ToString()
        {
            return ReportId + "-" + IsSolved;
        }
    }
}
