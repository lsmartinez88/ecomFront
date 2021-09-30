using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.PricingViewModels
{
    public class ListingListViewModel
    {
        public ListingListViewModel()
        {
        }
        public Models.DbFirstModels.Search Search{ get; set; }
        public Execution Execution { get; set; }
        public List<Criterion> Criteria{ get; set; }
    }
}
