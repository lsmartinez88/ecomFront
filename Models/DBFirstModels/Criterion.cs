using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Criterion
    {
        public Criterion()
        {
            CriteriaAttributes = new HashSet<CriteriaAttribute>();
        }

        public long IdCriteria { get; set; }
        public long Version { get; set; }
        public string CategoryId { get; set; }
        public long? Quantity { get; set; }
        public string RelatedLink { get; set; }
        public long SearchId { get; set; }
        public string SearchCriteria { get; set; }
        public string ItemCondition { get; set; }
        public virtual Category Category { get; set; }
        public virtual Search Search { get; set; }
        public virtual ICollection<CriteriaAttribute> CriteriaAttributes { get; set; }
    }
}
