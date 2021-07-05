﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class PostCategoryMap
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Post Post { get; set; }
    }
}
