using ecomFront.Common;
using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class CriteriaItemList
    {
        public CriteriaItemList(Criterion _criteria)
        {
            criteria = _criteria;
        }
        public string rutaCategorias { get; set; }
        public Criterion criteria { get; set; }
    }
}
