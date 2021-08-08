using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models
{
    public partial class ItemGrouping
    {
        public int Id { get; set; }
        public long Version { get; set; }
        public string GroupDescription { get; set; }
        public string GroupingType { get; set; }
        public int IdGrouping { get; set; }
        public string Nameml { get; set; }
        public  string  NamemlShort { get; set; }

        public virtual ICollection<ListingGrouping> ListingGroupings { get; set; }
    }
}
