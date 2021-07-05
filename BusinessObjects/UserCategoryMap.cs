using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class UserCategoryMap
    {
        public string Username { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User UsernameNavigation { get; set; }
    }
}
