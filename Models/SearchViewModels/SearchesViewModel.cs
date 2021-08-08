using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class SearchesViewModel
    {
        public SearchesViewModel()
        {
            RelatedItems = new List<SearchRelatedItem>();
        }

        public List<Search> Searches { get; set; }

        public List<SearchRelatedItem> RelatedItems { get; set; }
    }
}
