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
<<<<<<< HEAD
<<<<<<< HEAD
        IEnumerable<Post> GetPostsByBookmark(string username, int pageIndex);
=======
>>>>>>> 0e2125d (CommentRepo, BookmarkRepo)
=======
>>>>>>> 35e68c0 (Revert "Revert "CommentRepo, BookmarkRepo"")
    }
}
