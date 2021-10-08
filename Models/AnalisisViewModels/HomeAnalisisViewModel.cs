using ecomFront.Models.DbFirstModels;
using ecomFront.Models.ListingViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.AnalisisViewModels
{
    public class HomeAnalisisViewModel
    {
        public HomeAnalisisViewModel()
        {
        }
        public Models.DbFirstModels.Search Search{ get; set; }
        public Execution Execution { get; set; }
        public List<Criterion> Criteria{ get; set; }
        public List<ListingIndicador> Indicadores { get; set; }

        public List<ListingToDataTable> Listing { get; set; }

        public List<BarChartOportunity> Words { get; set; }

        public List<TrendsTreemap> Trends { get; set; }

        public List<EventsIndicator> EventsIndicator { get; set; }

    }
}
