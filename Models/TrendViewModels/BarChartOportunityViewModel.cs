using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.TrendViewModels
{
    public class BarChartOportunityViewModel
    {

        public BarChartOportunityViewModel()
        {
            data = new List<BarChartOportunityItem>();
        }
        public List<BarChartOportunityItem> data { get; set; }

        public double minValue { get; set; }

        public double maxValue { get; set; }
    }
}
