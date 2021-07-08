using BusinessObjects;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public void AddComment(Comment cmt) => CommentDAO.Instance.AddComment(cmt);

        public void DeleteComment(Comment cmt) => CommentDAO.Instance.DeleteComment(cmt);

        public void EditComment(Comment editedCmt) => CommentDAO.Instance.EditComment(editedCmt);

        public Comment GetComment(int cmtId) => CommentDAO.Instance.GetCommentByID(cmtId);

        public IEnumerable<Comment> GetCommentsByPost(int postId) => CommentDAO.Instance.GetCommentsByPosts(postId);

        public int GetMaxCommentId() => CommentDAO.Instance.GetMaxCommentId();

    }
}
