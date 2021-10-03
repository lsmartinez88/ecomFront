using ecomFront.Common;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class ItemSearchCategoryResult
    {
        public ItemSearchCategoryResult()
        {
            
        }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public List<ItemSearchCategorySelectedAttribute> attributes { get; set; }
        public string categoryQtty { get; set; }
    }
}
