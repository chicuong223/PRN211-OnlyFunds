using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class UserCategoryMap
    {
        private static UserCategoryMap instance = null;
        private static readonly object instanceLock = new object();

        public static UserCategoryMap Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserCategoryMap();
                    }

                    return instance;
                }
            }
        }
    }
}
