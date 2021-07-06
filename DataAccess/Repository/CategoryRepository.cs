using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetCategories(int pageIndex) => CategoryDAO.Instance.GetCategories(pageIndex);
        public Category GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id);
    }
}
