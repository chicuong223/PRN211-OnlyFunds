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
        void InsertPost(Post post);
        void DeletePost(int postId);
        Post GetPostById(int postId);
        IEnumerable<Post> SearchPostsByTitle(string title);
<<<<<<< HEAD
        IEnumerable<Post> GetAllPost(int pageIndex);
=======
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
    }
}
