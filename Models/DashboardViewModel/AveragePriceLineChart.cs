using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.DashboardViewModel
{
    public class AveragePriceLineChart
    {
        public AveragePriceLineChart()
        {
            xList = new List<string>();
            dataLegend = new List<string>();
            dataSeries = new List<AveragePriceLineSeries>();
        }

        public List<string> dataLegend { get; set; }
        public List<string> xList { get; set; }
        public List<AveragePriceLineSeries> dataSeries { get; set; }
    }
}
