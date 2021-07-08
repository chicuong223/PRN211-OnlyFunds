using BusinessObjects;
using DataAccess.IRepository;
using DataAccess.Repository;
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
            IPostCategoryMapRepository mapRepo = new PostCategoryMapRepository();
            mapRepo.DeleteMap(14, 1);
        }
    }
}
