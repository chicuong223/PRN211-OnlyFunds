using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        User GetUserByName(string username);
        User CheckLogin(string username, string password);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string username);
        void ChangePassword(User user, string newPassword);
        IEnumerable<User> GetUsers(int pageIndex);
    }
}
