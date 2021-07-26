using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class CategoryAttributeValue
    {
        public long IdCategoryAttributeValue { get; set; }
        public long Version { get; set; }
        public long CategoryAttributeId { get; set; }
        public string IdValueml { get; set; }
        public string Nameml { get; set; }

        public virtual CategoryAttribute CategoryAttribute { get; set; }
    }
}
