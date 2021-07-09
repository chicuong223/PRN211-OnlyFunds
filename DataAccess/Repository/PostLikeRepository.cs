using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class PostLikeRepository :IPostLikeRepository
    {
        public PostLike GetPostLike(string username, int postId) => PostLikeDAO.Instance.GetPostLike(username, postId);
        public void AddPostLike(PostLike like) => PostLikeDAO.Instance.AddPostLike(like);
        public void DeleteLike(string username, int postId) => PostLikeDAO.Instance.DeleteLike(username, postId);
        public int CountPostLike(int postId) => PostLikeDAO.Instance.CountPostLike(postId);
    }
}
