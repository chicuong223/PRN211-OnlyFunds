using BusinessObjects;
using DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PRN211_OnlyFundsContext();

            UserDAO dao = new UserDAO();
            User user = dao.GetUserByUsername("chicuong");
            PostDAO postDAO = new PostDAO();
            List<Post> postList = postDAO.GetPostsByUser(user, 1);
            postList.ForEach(p => Console.WriteLine(p.PostDescription));
        }
    }
}
