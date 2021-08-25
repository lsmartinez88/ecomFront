using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.MLModels
{
    public class Catalog
    {
        public string id { get; set; }
        public BuyBoxWinner buy_box_winner { get; set; }
    }

    public class BuyBoxWinner
    {
        public string item_id { get; set; }
    }
}