using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects
{
    public partial class Post
    {
        public Post()
        {
            Bookmarks = new HashSet<Bookmark>();
            CategoryMaps = new HashSet<CategoryMap>();
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public string FileUrl { get; set; }
        public string UploaderUsername { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual User UploaderUsernameNavigation { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<CategoryMap> CategoryMaps { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
