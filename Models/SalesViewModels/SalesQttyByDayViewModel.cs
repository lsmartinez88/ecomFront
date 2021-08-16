using ecomFront.Models.PricingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SalesViewModels
{
    public class SalesQttyByDayViewModel
    {
        public SalesQttyByDayViewModel()
        {
            items = new List<SalesQttyByDayInformation>();
        }
        public List<SalesQttyByDayInformation> items { get; set; }
        public double minValue { get; set; }
        public double maxValue { get; set; }
    }
}
