using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    class PostCategoryMap
    {
        private static PostCategoryMap instance = null;
        private static readonly object instanceLock = new object();

        public static PostCategoryMap Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostCategoryMap();
                    }

                    return instance;
                }
            }
        }

        public IEnumerable<Post> FilterPostByCategory(int categoryId)
        {
            var posts = new List<Post>();

            return posts;
        }
    }
}
