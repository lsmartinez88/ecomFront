using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class ExecutionsListViewModel
    {
        public ExecutionsListViewModel()
        {
            ExecutionsList = new List<Execution>();
        }
        public List<Execution> ExecutionsList { get; set; }
        public Models.DbFirstModels.Search search{ get; set; }
    }
}
