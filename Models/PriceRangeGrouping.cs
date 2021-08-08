using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_price_range_grouping")]
    public class PriceRangeGrouping
    {
        public int CriteriaId { get; set; }
        public int ExecutionId { get; set; }
        public int RangoDesde { get; set; }
        public int RangoHasta { get; set; }
        public String GroupingType { get; set; }
        public int ItemGroupingId { get; set; }
        public virtual ItemGrouping ItemGrouping { get; set; }
        public int CantidadPublicaciones { get; set; }
        public int CantidadVentas { get; set; }
    }
}
