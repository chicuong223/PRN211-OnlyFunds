using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class CommentLike
    {
        public int CommentId { get; set; }
        public string Username { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
