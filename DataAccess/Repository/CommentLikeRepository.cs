using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class CommentLikeRepository : ICommentLikeRepository
    {
        public CommentLike GetCommentLike(string username, int commentId) =>
            CommentLikeDAO.Instance.GetCommentLike(username, commentId);

        public void AddCommentLike(CommentLike like) => CommentLikeDAO.Instance.AddCommentLike(like);
        public void DeleteLike(string username, int postId) => CommentLikeDAO.Instance.DeleteLike(username, postId);

        public CommentLike CheckCommentLike(string username, int commentId) =>
            CommentLikeDAO.Instance.CheckCommentLike(username, commentId);

        public IEnumerable<CommentLike> GetCommentLikeByCommentId(int commentId) =>
            CommentLikeDAO.Instance.GetCommentLikeByCommentId(commentId);
    }
}