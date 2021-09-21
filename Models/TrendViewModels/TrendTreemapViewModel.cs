using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.TrendViewModels
{
    public class TrendTreemapViewModel
    {

        public TrendTreemapViewModel()
        {
            items = new List<TrendTreemapItem>();
        }
        public List<TrendTreemapItem> items { get; set; }
    }
}
