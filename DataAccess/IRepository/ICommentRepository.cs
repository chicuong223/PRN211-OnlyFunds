using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ICommentRepository
    {
        void AddComment(Comment cmt);
        void DeleteComment(Comment cmt);
        void EditComment(Comment editedCmt);
        Comment GetComment(int cmtId);
        IEnumerable<Comment> GetCommentsByPost(int postId);

    }
}
