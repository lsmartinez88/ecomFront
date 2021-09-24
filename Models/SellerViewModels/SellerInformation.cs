using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SellerViewModels
{
    public class SellerInformation
    {
        public SellerInformation()
        {
            SellerData = new List<TopSellers>();
        }

        public string sellerId { get; set; }
        public List<TopSellers> SellerData { get; set; }
        public string style { get; set; }

    }
}
