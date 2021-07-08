using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers(int pageIndex) => UserDAO.Instance.GetUsers(pageIndex);
        public User GetUserByName(string username) => UserDAO.Instance.GetUserByName(username);
        public User CheckLogin(string username, string password) => UserDAO.Instance.CheckLogin(username, password);
        public User GetUserByEmail(string email) => UserDAO.Instance.GetUserByEmail(email);
        public void AddUser(User user) => UserDAO.Instance.AddUser(user);
        public void UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
        public void ChangePassword(User user, string newPassword) => UserDAO.Instance.ChangePassword(user, newPassword);
        public void DeleteUser(string username) => UserDAO.Instance.DeleteUser(username);
    }
}
