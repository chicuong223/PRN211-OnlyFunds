using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class ReportDAO
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
    }
}
