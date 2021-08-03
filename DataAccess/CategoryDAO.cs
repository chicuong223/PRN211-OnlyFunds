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
        //--------Checked
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
        //--------Checked
        public Category GetCategoryById(int id)
        {
            Category category = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                category = context.Categories.SingleOrDefault(c => c.CategoryId == (id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return category;
        }
        //-------Viet them ham create category
        public IEnumerable<Category> GetCategoriesByPost(int postID)
        {
            List<Category> catList = new List<Category>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                IEnumerable<int> categoryID = context.PostCategoryMaps.Where(map => map.PostId == postID).Select(map => map.CategoryId).ToList();
                foreach (int catID in categoryID)
                {
                    Category cat = GetCategoryById(catID);
                    catList.Add(cat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            return catList;
        }
    }
}
