using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SalesViewModels
{
    public class HomeSalesViewModel
    {
        public HomeSalesViewModel()
        {
        }
        public Search Search{ get; set; }
        public Execution Execution { get; set; }
        public List<Criterion> Criteria{ get; set; }
        public List<SalesQuantityMap> SalesPerCityComprador { get; set; }
        public List<SalesQuantityMap> SalesPerCityVendedor { get; set; }
    }
}
