using BusinessObjects;

namespace DataAccess
{
    public interface IAdminRepository
    {
        Admin CheckLogin(string username, string password);
    }
}