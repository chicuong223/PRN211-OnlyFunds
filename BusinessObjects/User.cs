using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class User
    {
        public User()
        {
            Bookmarks = new HashSet<Bookmark>();
            CommentLikes = new HashSet<CommentLike>();
            Comments = new HashSet<Comment>();
            PostLikes = new HashSet<PostLike>();
            Posts = new HashSet<Post>();
            UserCategoryMaps = new HashSet<UserCategoryMap>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserCategoryMap> UserCategoryMaps { get; set; }
    }
}
