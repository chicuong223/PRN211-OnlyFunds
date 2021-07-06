using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class UserCategoryMapRepository : IUserCategoryMapRepository
    {
        public void AddUserCategoryMap(UserCategoryMap map) => UserCategoryMapDAO.Instance.AddUserCategoryMap(map);

        public UserCategoryMap GetUserCategoryMap(string username, int categoryId) =>
            UserCategoryMapDAO.Instance.GetUserCategoryMap(username, categoryId);

        public void DeleteCategoryMap(string username, int categoryId) =>
            UserCategoryMapDAO.Instance.DeleteCategoryMap(username, categoryId);
    }
}
