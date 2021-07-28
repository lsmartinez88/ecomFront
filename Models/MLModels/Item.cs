using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.MLModels
{
    public class Item
    {
        public String Id { get; set; }
        public String Title { get; set; }
        public String Seller_Id { get; set; }
        public String Category_Id { get; set; }
        public Double Price { get; set; }
        public String Currency_Id { get; set; }
        public int Initial_Quantity { get; set; }
        public int Available_Quantity { get; set; }
        public int Sold_Quantity { get; set; }
        public String Buying_Mode { get; set; }
        public String Listing_Type_Id { get; set; }
        public String Start_Time { get; set; }
        public String Condition { get; set; }
        public List<Attributes> Attributes { get; set; }
        public List<String> Tags { get; set; }

    }

    public class Attributes
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Value_Id { get; set; }
        public String Value_Name { get; set; }
        public String Value_Struct { get; set; }
        public List<Values> Values { get; set; }
        public String Attribute_Group_Id { get; set; }
        public String Attribute_Group_Name { get; set; }
    }

    public class Values
    {
        public String Id { get; set;}
        public String Name { get; set; }
        public String Struct { get; set; }
    }
}
