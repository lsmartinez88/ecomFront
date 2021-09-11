using ecomFront.Models.PricingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.TrendViewModels
{
    public class WordCloudViewModel
    {
        public WordCloudViewModel()
        {
            items = new List<WordCloudItem>();
        }
        public List<WordCloudItem> items { get; set; }
    }
}
