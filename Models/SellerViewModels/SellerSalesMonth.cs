using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SellerViewModels
{
    public class SellerSalesMonth
    {
        public SellerSalesMonth()
        {
        }

        public string sellerId { get; set; }
        public String month { get; set; }
        public int cantidad { get; set; }

    }
}
