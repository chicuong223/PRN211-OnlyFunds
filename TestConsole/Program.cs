using System;
using System.Collections.Generic;
using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserRepository userRepository = new UserRepository();
            ICategoryRepository categoryRepository = new CategoryRepository();
            IPostCategoryMapRepository map = new PostCategoryMapRepository();
            Console.WriteLine(map.FilterPostByCategory(1,1));
        }
    }
}
