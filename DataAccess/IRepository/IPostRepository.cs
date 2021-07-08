using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.IRepository
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPostByUser(User user, int pageIndex);
        int CountPostByUser(User user);
        int CountPostByCategory(Category category);
        void InsertPost(Post post);
        void DeletePost(int postId);
        Post GetPostById(int postId);
        IEnumerable<Post> SearchPostsByTitle(string title);
        IEnumerable<Post> GetAllPost(int pageIndex);
        int GetMaxPostId();
        int CountAllPost();
        void UpdatePost(Post editedPost);
    }
}
