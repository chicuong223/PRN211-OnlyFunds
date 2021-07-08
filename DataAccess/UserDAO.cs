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
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }

                    return instance;
                }
            }
        }
        //----------------------Checked
        public User GetUserByName(string username)
        {
            User user = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                user = context.Users.FirstOrDefault(u => u.Username.Equals(username));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return user;
        }
        //----------------------- Checked
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
        //-----------------------Checked
        public User GetUserByEmail(string email)
        {
            User user = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                user = context.Users.FirstOrDefault(u => u.Email.Equals(email));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return user;
        }
        //-------------------Checked
        public void AddUser(User user)
        {
            try
            {
                User _user = GetUserByName(user.Username);
                User _email = GetUserByEmail(user.Email);
                if (_user == null && _email == null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //--------------------Checked
        public void UpdateUser(User user)
        {
            try
            {
                User _user = GetUserByName(user.Username);
                if (_user != null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User does not exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //---------------Checked
        public void DeleteUser(string username)
        {
            try
            {
                User _user = GetUserByName(username);
                if (_user != null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.Users.Remove(_user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User does not exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //-------------Checked
        public IEnumerable<User> GetUsers(int pageIndex)
        {
            var users = new List<User>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                users = context.Users.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return users;
        }

        public void ChangePassword(User user, string newPassword)
        {
            try
            {
                user.Password = newPassword;
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Users.Update(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
