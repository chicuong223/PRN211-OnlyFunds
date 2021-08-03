using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.IRepository
{
    public interface IPostLikeRepository
    {
        PostLike GetPostLike(string username, int postId);

        void AddPostLike(PostLike like);
        void DeleteLike(string username, int postId);
        int CountPostLike(int postId);
        PostLike CheckUserLike(string username, int id);
    }
}
