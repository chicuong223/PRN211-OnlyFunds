using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.IRepository
{
    public interface ICommentLikeRepository
    {
        CommentLike GetCommentLike(string username, int commentId);
        void AddCommentLike(CommentLike like);
        void DeleteLike(string username, int postId);
        CommentLike CheckCommentLike(string username, int commentId);
        IEnumerable<CommentLike> GetCommentLikeByCommentId(int commentId);
    }
}