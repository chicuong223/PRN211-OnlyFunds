using BusinessObjects;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public void AddBookmark(string username, int postId) => BookmarkDAO.Instance.AddBookmark(postId, username);

        public void DeleteBookmark(Bookmark bookmark) => BookmarkDAO.Instance.DeleteBookmark(bookmark);

        public Bookmark GetBookmark(string username, int postId) => BookmarkDAO.Instance.GetBookmark(postId, username);

        public IEnumerable<Post> GetPostsByBookmark(string username, int pageIndex)
        => BookmarkDAO.Instance.GetPostsByBookmark(username, pageIndex);

        public int CountBookMarkPost(string username) => BookmarkDAO.Instance.CountBookMarkPost(username);
    }
}
