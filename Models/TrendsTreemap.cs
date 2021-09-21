using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models
{
    [Table("front_trends_treemap")]
    public class TrendsTreemap
    {
        public int ExecutionId { get; set; }
        public String TrendName { get; set; }
        public String TrendPadre { get; set; }
        public double? IndiceSize { get; set; }
        public double? IndiceColor { get; set; }
        public int? CantidadVentas { get; set; }
        public int? CantidadPublicaciones { get; set; }
        public int PosicionTendencia { get; set; }
        public double? VentasPorPublicacion { get; set; }

    }
}
