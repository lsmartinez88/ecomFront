using ecomFront.Models.PricingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.TrendViewModels
{
    public class WordCloudViewModel2
    {
        public WordCloudViewModel2()
        {
            items = null;
        }
        public String[,] items { get; set; }
    }
}
