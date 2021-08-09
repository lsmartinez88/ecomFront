using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.PricingViewModels
{
    public class ScatterPunchInformation
    {
        public ScatterPunchInformation()
        {
            xList = new List<string>();
            yList = new List<string>();
        }
        public List<string> xList { get; set; }
        public string xName { get; set; }
        public List<string> yList { get; set; }
        public string yName { get; set; }
        public string dataName { get; set; }
        public string data { get; set; }
        public string data2 { get; set; }
        public int maxValue { get; set; }
        public int minValue { get; set; }
    }
}
