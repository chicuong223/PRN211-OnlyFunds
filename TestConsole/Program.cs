using BusinessObjects;
using DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PRN211_OnlyFundsContext();
            SqlConnection cnn = (SqlConnection)context.Database.GetDbConnection();
            Console.WriteLine(cnn);
            UserDAO dao = new UserDAO();
            Console.WriteLine(dao.CheckLogin("chicuong", "12345"));
        }
    }
}
