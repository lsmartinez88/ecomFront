
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.ListingViewModels
{
    public class ListingExecutionDashboardModel
    {
        public ListingExecutionDashboardModel()
        {
            Pictures = new List<Picture>();
        }
        
        public String IdMl { get; set; }
        public double? Price { get; set; }
        public long? SoldQuantity { get; set; }
        public string Title { get; set; }
        public long? TotalQuestions { get; set; }
        public long? VisitsQuantity { get; set; }
        public long? ReviewsQuantity { get; set; }
        public double? IndexVisitsxVentas { get; set; }
        public double? IndexQuestionsxVentas { get; set; }

        public List<Picture> Pictures { get; set; }

    }
}
