using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    class UserCategoryMapDAO
    {
        private static UserCategoryMapDAO instance = null;
        private static readonly object instanceLock = new object();

        public static UserCategoryMapDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserCategoryMapDAO();
                    }

                    return instance;
                }
            }
        }
        //-------------------
        public void AddUserCategoryMap(UserCategoryMap map)
        {
            UserCategoryMap _map = GetUserCategoryMap(map.Username, map.CategoryId);
            try
            {
                if (_map == null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.UserCategoryMaps.Add(map);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Already exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public UserCategoryMap GetUserCategoryMap(string username, int categoryId)
        {
            UserCategoryMap map = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                map = context.UserCategoryMaps.SingleOrDefault(m =>
                    m.Username.Equals(username) && m.CategoryId.Equals(categoryId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return map;
        }

        public void DeleteCategoryMap(string username, int categoryId)
        {
            try
            {
                UserCategoryMap map = GetUserCategoryMap(username, categoryId);
                if (map != null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.UserCategoryMaps.Remove(map);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Do not exist");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
