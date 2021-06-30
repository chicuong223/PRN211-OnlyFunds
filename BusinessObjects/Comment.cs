﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime? CommentDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
