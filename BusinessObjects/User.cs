using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects
{
    public partial class User
    {
        public User()
        {
            Bookmarks = new HashSet<Bookmark>();
            Comments = new HashSet<Comment>();
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
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserCategoryMap> UserCategoryMaps { get; set; }
    }
}
