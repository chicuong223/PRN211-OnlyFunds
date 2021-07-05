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
            Comments = new HashSet<Comment>();
            PostCategoryMaps = new HashSet<PostCategoryMap>();
            PostLikes = new HashSet<PostLike>();
            PostReports = new HashSet<PostReport>();
        }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public string FileUrl { get; set; }
        public string UploaderUsername { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual User UploaderUsernameNavigation { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostCategoryMap> PostCategoryMaps { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
