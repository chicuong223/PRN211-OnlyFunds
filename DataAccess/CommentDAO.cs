using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class CommentDAO
    {
        private static CommentDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CommentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommentDAO();
                    }

                    return instance;
                }
            }
        }

        public IEnumerable<Comment> GetCommentsByPosts(int postId)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                return context.Comments.Where(cmt => cmt.PostId == postId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Comments.Add(comment);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteComment(Comment comment)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditComment(Comment updatedComment)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                context.Entry(updatedComment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Comment GetCommentByID(int commentID)
        {
            Comment _comment = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                _comment = context.Comments.Where(cmt => cmt.CommentId == commentID).Single();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _comment;
        }

        public int GetMaxCommentId()
        {
            int commentId = 0;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                commentId = context.Comments.Max(c => c.CommentId);
                if (commentId == 0)
                {
                    throw new Exception("no comment");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return commentId;
        }
    }
}
