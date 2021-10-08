using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.PricingViewModels
{
    public class SingleAxisScatterInformation
    {
        public SingleAxisScatterInformation()
        {
            xList = new List<string>();
        }
        public List<string> xList { get; set; }
        public string dataPublicaciones { get; set; }
        public string dataVentas { get; set; }
        public int maxValueVentas { get; set; }
        public int maxValuePublicaciones { get; set; }
    }
}
