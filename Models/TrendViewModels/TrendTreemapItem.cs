using ecomFront.Models.PricingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.TrendViewModels
{
    public class TrendTreemapItem
    {
        public String  Trend { get; set; }
        public String Parent { get; set; }
        public double? Size { get; set; }
        public double? Color { get; set; }
        public int? CantidadVentas { get; set; }
        public int? CantidadPublicaciones { get; set; }
        public int? PosicionTendencia { get; set; }
        public double? VentasPorPublicacion { get; set; }

    }
}
