using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IBookmarkRepository
    {
        void AddBookmark(string username, int postId);
        Bookmark GetBookmark(string username, int postId);
        void DeleteBookmark(Bookmark bookmark);
        IEnumerable<Post> GetPostsByBookmark(string username, int pageIndex);
        int CountBookMarkPost(string username);
    }
}
