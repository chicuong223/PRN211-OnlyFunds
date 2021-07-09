using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public Admin CheckLogin(string username, string password) => AdminDAO.Instance.CheckLogin(username, password);
        public Admin GetAdminByUname(string username) => AdminDAO.Instance.GetAdminByUname(username);
    }
}
