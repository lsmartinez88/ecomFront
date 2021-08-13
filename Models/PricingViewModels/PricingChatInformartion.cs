using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.PricingViewModels
{
    public class PricingChatInformartion
    {
        public PricingChatInformartion()
        {
            Prices = new List<double>();
            Dates = new List<string>();
        }
        public List<double> Prices { get; set; }
        public List<string> Dates{ get; set; }
    }
}
