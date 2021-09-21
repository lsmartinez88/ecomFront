using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_top_sellers")]
    public class TopSellers
    {
        public int ExecutionId { get; set; }
        public String SellerId { get; set; }
        public String ParameterName { get; set; }
        public String ParameterValue { get; set; }
    }
}
