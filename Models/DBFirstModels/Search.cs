using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Search
    {
        public Search()
        {
            Criteria = new HashSet<Criterion>();
            Executions = new HashSet<Execution>();
        }

        public long IdSearch { get; set; }
        public long Version { get; set; }
        public string Description { get; set; }
        public string SearchType { get; set; }
        public string UserId { get; set; }
        public string CatalogProductIdml { get; set; }
        public string ListingPermalink { get; set; }

        public virtual ICollection<Criterion> Criteria { get; set; }
        public virtual ICollection<Execution> Executions { get; set; }
    }
}
