using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        public User CheckLogin(string username, string password)
        {
            User user = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                user = (User)context.Users.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                user = (from u in context.Users.ToList()
                            where u.Username.Equals(username)
                            select u).Single();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
