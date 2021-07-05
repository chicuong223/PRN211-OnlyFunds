using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }

                    return instance;
                }
            }
        }
        public IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                categories = context.Categories.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category category = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                category = context.Categories.SingleOrDefault(c => c.CategoryId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return category;
        }
    }
}
