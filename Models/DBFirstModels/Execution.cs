using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Execution
    {
        public long IdExecution { get; set; }
        public long Version { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateExecuted { get; set; }
        public DateTime? DateFinished { get; set; }
        public string ErrorDescription { get; set; }
        public string ExecutionStatus { get; set; }
        public long SearchId { get; set; }
        public long ListingQtty { get; set; }

        public virtual Search Search { get; set; }
    }
}
