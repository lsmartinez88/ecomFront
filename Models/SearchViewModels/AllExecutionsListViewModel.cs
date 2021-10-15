using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class AllExecutionsListViewModel
    {
        public AllExecutionsListViewModel()
        {
            List = new List<ExecutionsListViewModel>();
        }
        public List<ExecutionsListViewModel> List { get; set; }
    }
}
