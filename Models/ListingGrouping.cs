using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_listing_group")]
    public class ListingGrouping
    {
        public int CriteriaId { get; set; }
        public int ExecutionId { get; set; }
        public String GroupingType { get; set; }
        public String Clasification { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
