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

        public IEnumerable<Comment> GetCommentsByPosts(Post post)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                return context.Comments.Where(cmt => cmt.PostId == post.PostId);
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

        public IEnumerable<Comment> GetCommentsByPosts(int postId)
        {
            List<Comment> lst = new List<Comment>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                lst = context.Comments.Where(cmt => cmt.PostId == postId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lst;
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
    }
}
