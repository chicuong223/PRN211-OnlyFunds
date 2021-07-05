using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Category
    {
        public Category()
        {
            PostCategoryMaps = new HashSet<PostCategoryMap>();
            UserCategoryMaps = new HashSet<UserCategoryMap>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<PostCategoryMap> PostCategoryMaps { get; set; }
        public virtual ICollection<UserCategoryMap> UserCategoryMaps { get; set; }
    }
}
