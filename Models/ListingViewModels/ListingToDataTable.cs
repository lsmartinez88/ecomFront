using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.ListingViewModels
{
    public class ListingToDataTable
    {
        public ListingToDataTable()
        {
        }
        public long ExecutionId { get; set; }
        public string IdMl { get; set; }
        public long? AvailableQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public string ListingCondition { get; set; }
        public double? OriginalPrice { get; set; }
        public string Permalink { get; set; }
        public double? Price { get; set; }
        public long SellerIdMl { get; set; }
        public long? SoldQuantity { get; set; }
        public string Title { get; set; }
        public long? TotalQuestions { get; set; }
        public long? VisitsQuantity { get; set; }
        public long? ReviewsQuantity { get; set; }
        public double? RatingAverage { get; set; }
        public ulong Elegible { get; set; }
        public string ListingTypeId { get; set; }

        public string CategoryId { get; set; }
        public string Thumbnail { get; set; }


        public virtual Category Category { get; set; }


    }
}
