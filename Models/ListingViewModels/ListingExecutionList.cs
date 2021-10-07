using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.ListingViewModels
{
    public class ListingExecutionList
    {
        public ListingExecutionList()
        {
            Items = new List<ListingExecutionDashboardModel>();
        }

        public List<ListingExecutionDashboardModel> Items { get; set; }
    }
}
