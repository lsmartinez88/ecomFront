using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class CriteriaListViewModel
    {
        public CriteriaListViewModel()
        {
            criteriaItems = new List<CriteriaItemList>();
        }
        public List<CriteriaItemList> criteriaItems { get; set; }
        public Search search { get; set; }
    }
}
