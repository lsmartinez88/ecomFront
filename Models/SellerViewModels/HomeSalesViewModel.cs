using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using ecomFront.Models.SellerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SellerViewModel
{
    public class HomeSellerViewModel
    {
        public HomeSellerViewModel()
        {
            SellerInformation = new List<SellerInformation>();
        }
        public Search Search{ get; set; }
        public Execution Execution { get; set; }
        public List<Criterion> Criteria{ get; set; }
        public List<SellerInformation> SellerInformation { get; set; }

    }
}
