using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class CommentDAO
    {
        private static CommentDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CommentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommentDAO();
                    }

                    return instance;
                }
            }
        }
    }
}
