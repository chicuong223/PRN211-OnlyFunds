using BusinessObjects;
using System;
using System.Linq;

namespace DataAccess
{
    public class AdminDAO
    {
        private static AdminDAO instance = null;
        private static readonly object instanceLock = new object();

        public static AdminDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AdminDAO();
                    }
                    return instance;
                }
            }
        }

        public Admin GetAdminByUname(string username)
        {
            Admin admin = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                admin = context.Admins.SingleOrDefault(ad => ad.Username.Equals(username));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return admin;
        }
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
