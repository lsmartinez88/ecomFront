using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class CategoryAttribute
    {
        public CategoryAttribute()
        {
            CategoryAttributeValues = new HashSet<CategoryAttributeValue>();
        }

        public long IdCategoryAttribute { get; set; }
        public long Version { get; set; }
        public string AttributeGroupIdml { get; set; }
        public string AttributeGroupNameml { get; set; }
        public string CategoryId { get; set; }
        public string IdAttributeml { get; set; }
        public string Nameml { get; set; }
        public string ValueTypeml { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CategoryAttributeValue> CategoryAttributeValues { get; set; }
    }
}
