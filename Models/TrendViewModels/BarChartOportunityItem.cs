using ecomFront.Models.PricingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.TrendViewModels
{
    public class BarChartOportunityItem
    {
        public String  Palabra { get; set; }
        public int CantidadApariciones { get; set; }
        public double Indice { get; set; }
        public int MejorTendencia { get; set; }

    }
}
