using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects
{
    public partial class PostLike
    {
        public int PostId { get; set; }
        public string Username { get; set; }

        public virtual Post Post { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
