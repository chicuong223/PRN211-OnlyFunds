using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    public class AdminDAO
    {
        public Admin CheckLogin(string username, string password)
        {
            Admin user = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                user = (Admin)context.Admins.Where(u => u.Username.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
