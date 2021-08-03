using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    class CommentLikeDAO
    {
        private static CommentLikeDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CommentLikeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommentLikeDAO();
                    }

                    return instance;
                }
            }
        }
        //-------------------------
        public CommentLike GetCommentLike(string username, int commentId)
        {
            CommentLike commentLike = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                commentLike =
                    context.CommentLikes.SingleOrDefault(l => l.Username.Equals(username) && l.CommentId == commentId);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return commentLike;
        }
        public IEnumerable<CommentLike> GetCommentLikeByCommentId(int commendId)
        {
            var commentLikeList = new List<CommentLike>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                commentLikeList = context.CommentLikes.Where(l => l.CommentId == commendId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return commentLikeList;
        }
        public void AddCommentLike(CommentLike like)
        {
            try
            {
                CommentLike _like = GetCommentLike(like.Username, like.CommentId);
                if (_like == null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.CommentLikes.Add(like);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Already liked");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteLike(string username, int postId)
        {
            try
            {
                CommentLike like = GetCommentLike(username, postId);
                if (like != null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.CommentLikes.Remove(like);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public CommentLike CheckCommentLike(string username, int commentId) 
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                CommentLike like =
                    context.CommentLikes.FirstOrDefault(l => l.Username.Equals(username) && l.CommentId == commentId);
                return like;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
