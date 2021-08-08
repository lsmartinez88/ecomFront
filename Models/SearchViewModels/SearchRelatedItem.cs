using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class SearchRelatedItem 
    {
        public Item item { get; set; }
        public long searchId { get; set; }
    }
}
