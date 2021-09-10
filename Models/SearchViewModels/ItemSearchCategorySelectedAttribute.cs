using ecomFront.Common;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class ItemSearchCategorySelectedAttribute
    {
        public ItemSearchCategorySelectedAttribute()
        {
            
        }

        public string value_id { get; set; }
        public string value { get; set; }
        public string value_name { get; set; }
        public string quantity { get; set; }
        public string name { get; set; }
    }
}