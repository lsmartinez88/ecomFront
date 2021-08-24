using ecomFront.Common;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class ItemSearchResultInformation
    {
        public ItemSearchResultInformation()
        {
            pictures = new List<Picture>();
            descriptions = new List<Description>();
            attributes = new List<MLModels.Attribute>();
        }
        public string id{ get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string category_id { get; set; }
        public CategoryML category { get; set; }
        public double? price { get; set; }
        public int sold_quantity { get; set; }
        public string date_created { get; set; }
        public string condition { get; set; }
        public string permalink { get; set; }
        public string thumbnail { get; set; }
        public List<Picture> pictures { get; set; }
        public List<Description> descriptions { get; set; }
        public List<MLModels.Attribute> attributes { get; set; }
    }
}
