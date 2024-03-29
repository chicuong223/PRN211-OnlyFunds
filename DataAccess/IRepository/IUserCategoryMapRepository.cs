﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUserCategoryMapRepository
    {
        void AddUserCategoryMap(UserCategoryMap map);
        UserCategoryMap GetUserCategoryMap(string username, int categoryId);
        void DeleteCategoryMap(string username, int categoryId);
    }
}
