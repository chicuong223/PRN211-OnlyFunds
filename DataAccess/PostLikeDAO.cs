using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess
{
    class PostLikeDAO
    {
        private static PostLikeDAO instance = null;

        private static readonly object instanceLock = new object();

        public static PostLikeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostLikeDAO();
                    }

                    return instance;
                }
            }
        }

        public PostLike GetPostLike(string username, int postId)
        {
            PostLike postLike = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                postLike = context.PostLikes.SingleOrDefault(l => l.Username.Equals(username) && l.PostId == postId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return postLike;
        }

        public void AddPostLike(PostLike like)
        {
            try
            {
                PostLike _like = GetPostLike(like.Username, like.PostId);
                if (_like == null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.PostLikes.Add(like);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Already Liked");
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
                PostLike like = GetPostLike(username, postId);
                if (like != null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.PostLikes.Remove(like);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int CountPostLike(int postId)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                int count = context.PostLikes.Count(l => l.PostId == postId);
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PostLike CheckUserLike(string username, int postId)
        {
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                PostLike like =
                    context.PostLikes.FirstOrDefault(l => l.Username.Equals(username) && l.PostId == postId);
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
