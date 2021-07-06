using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.IRepository
{
    public interface IPostCategoryMapRepository
    {
        IEnumerable<Post> FilterPostByCategory(int categoryId, int pageIndex);
        void AddPostMap(int postId, int categoryId);
        PostCategoryMap GetPostMap(int postId, int categoryId);
    }
}
