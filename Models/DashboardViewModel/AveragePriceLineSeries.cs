using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.DashboardViewModel
{
    public class AveragePriceLineSeries
    {
        public AveragePriceLineSeries()
        {
            yList = new List<string>();
        }

        public string legend { get; set; }
        public List<string> yList { get; set; }
    }
}
