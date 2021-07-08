using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class PostRepository : IPostRepository
    {
        public IEnumerable<Post> GetPostByUser(User user, int pageIndex) =>
            PostDAO.Instance.GetPostsByUser(user, pageIndex);

        public int CountPostByUser(User user) => PostDAO.Instance.CountPostsByUser(user);
        public int CountPostByCategory(Category category) => PostDAO.Instance.CountPostByCategory(category);
        public void InsertPost(Post post) => PostDAO.Instance.InsertPost(post);
        public void DeletePost(int postId) => PostDAO.Instance.DeletePost(postId);
        public Post GetPostById(int postId) => PostDAO.Instance.GetPostByID(postId);
        public IEnumerable<Post> GetAllPost(int pageIndex) => PostDAO.Instance.GetAllPost(pageIndex);
        public IEnumerable<Post> SearchPostsByTitle(string title) => PostDAO.Instance.SearchPostByTitle(title);
        public int GetMaxPostId() => PostDAO.Instance.GetMaxPostId();
        public int CountAllPost() => PostDAO.Instance.CountAllPost();
        public void UpdatePost(Post editedPost) => PostDAO.Instance.UpdatePost(editedPost);
        public IEnumerable<PostReport> GetReportsByPost(int postId) => ReportDAO.Instance.GetReportsByPost(postId);
    }
}
