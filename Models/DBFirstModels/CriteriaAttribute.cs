using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class CriteriaAttribute
    {
        public long IdCriteriaAttribute { get; set; }
        public long Version { get; set; }
        public long CriteriaId { get; set; }
        public string IdAttributeml { get; set; }
        public string IdAttributeValueml { get; set; }
        public string NameAttributeml { get; set; }
        public string NameAttributeValueml { get; set; }

        public virtual Criterion Criteria { get; set; }
    }
}
