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
                using var context = new PRN211_OnlyFundsContext();
                user = (User)context.Users.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
