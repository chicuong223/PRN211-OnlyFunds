using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects
{
    public partial class Category
    {
        public Category()
        {
            CategoryMaps = new HashSet<CategoryMap>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<CategoryMap> CategoryMaps { get; set; }
    }
}
