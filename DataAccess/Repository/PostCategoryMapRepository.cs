using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class PostCategoryMapRepository :IPostCategoryMapRepository
    {
        public IEnumerable<Post> FilterPostByCategory(int categoryId, int pageIndex) =>
            PostCategoryMapDAO.Instance.FilterPostByCategory(categoryId, pageIndex);

        public void AddPostMap(PostCategoryMap map) =>
            PostCategoryMapDAO.Instance.AddPostMap(map);

        public PostCategoryMap GetPostMap(int postId, int categoryId) =>
            PostCategoryMapDAO.Instance.GetPostMap(postId, categoryId);

        public void DeletePostMap(Post post, Category category) =>
            PostCategoryMapDAO.Instance.DeletePostMap(post, category);
    }
}
