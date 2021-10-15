using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using ecomFront.Models.SellerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SharedViewModels
{
    public class ActivitiesViewModel 
    {
        public ActivitiesViewModel()
        {
            ActivityInformation = new List<ActivityInformation>();
        }
        public List<ActivityInformation> ActivityInformation { get; set; }

    }
}
