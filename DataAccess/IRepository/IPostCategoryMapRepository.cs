using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.IRepository
{
    interface IPostCategoryMapRepository
    {
        IEnumerable<Post> FilterPostByCategory(int categoryId, int pageIndex);
        void AddPostMap(PostCategoryMap map);
        PostCategoryMap GetPostMap(int postId, int categoryId);
    }
}
