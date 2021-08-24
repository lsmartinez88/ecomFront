using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.MLModels
{
    public class Item
    {
        public string id { get; set; }
        public string site_id { get; set; }
        public string title { get; set; }
        public object subtitle { get; set; }
        public int seller_id { get; set; }
        public string category_id { get; set; }
        public object official_store_id { get; set; }
        public double? price { get; set; }
        public double? base_price { get; set; }
        public double? original_price { get; set; }
        public string currency_id { get; set; }
        public int initial_quantity { get; set; }
        public int available_quantity { get; set; }
        public int sold_quantity { get; set; }
        public Sale_Terms[] sale_terms { get; set; }
        public string buying_mode { get; set; }
        public string listing_type_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime stop_time { get; set; }
        public string condition { get; set; }
        public string permalink { get; set; }
        public string thumbnail_id { get; set; }
        public string thumbnail { get; set; }
        public string secure_thumbnail { get; set; }
        public Picture[] pictures { get; set; }
        public object video_id { get; set; }
        public Description[] descriptions { get; set; }
        public bool accepts_mercadopago { get; set; }
        public object[] non_mercado_pago_payment_methods { get; set; }
        public Shipping shipping { get; set; }
        public string international_delivery_mode { get; set; }
        public Seller_Address seller_address { get; set; }
        public object seller_contact { get; set; }
        public Location location { get; set; }
        public object[] coverage_areas { get; set; }
        public Attribute[] attributes { get; set; }
        public object[] warnings { get; set; }
        public string listing_source { get; set; }
        public Variation[] variations { get; set; }
        public string status { get; set; }
        public object[] sub_status { get; set; }
        public string[] tags { get; set; }
        public string warranty { get; set; }
        public string catalog_product_id { get; set; }
        public string domain_id { get; set; }
        public object parent_item_id { get; set; }
        public object differential_pricing { get; set; }
        public object[] deal_ids { get; set; }
        public bool automatic_relist { get; set; }
        public DateTime date_created { get; set; }
        public DateTime last_updated { get; set; }
        public float health { get; set; }
        public bool catalog_listing { get; set; }
        public string[] channels { get; set; }
    }

    public class Shipping
    {
        public string mode { get; set; }
        public Free_Methods[] free_methods { get; set; }
        public object[] tags { get; set; }
        public object dimensions { get; set; }
        public bool local_pick_up { get; set; }
        public bool free_shipping { get; set; }
        public string logistic_type { get; set; }
        public bool store_pick_up { get; set; }
    }

    public class Free_Methods
    {
        public int id { get; set; }
        public Rule rule { get; set; }
    }

    public class Rule
    {
        public bool _default { get; set; }
        public string free_mode { get; set; }
        public bool free_shipping_flag { get; set; }
        public object value { get; set; }
    }

    public class Seller_Address
    {
        public City city { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public Search_Location search_location { get; set; }
        public int id { get; set; }
    }

    public class City
    {
        public string name { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Search_Location
    {
        public Neighborhood neighborhood { get; set; }
        public City1 city { get; set; }
        public State1 state { get; set; }
    }

    public class Neighborhood
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class City1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class State1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
    }

    public class Sale_Terms
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public object value_struct { get; set; }
        public Value[] values { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public object _struct { get; set; }
    }

    public class Picture
    {
        public string id { get; set; }
        public string url { get; set; }
        public string secure_url { get; set; }
        public string size { get; set; }
        public string max_size { get; set; }
        public string quality { get; set; }
    }

    public class Description
    {
        public string id { get; set; }
    }

    public class Attribute
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public Value_Struct value_struct { get; set; }
        public Value1[] values { get; set; }
        public string attribute_group_id { get; set; }
        public string attribute_group_name { get; set; }
    }

    public class Value_Struct
    {
        public float number { get; set; }
        public string unit { get; set; }
    }

    public class Value1
    {
        public string id { get; set; }
        public string name { get; set; }
        public Struct _struct { get; set; }
    }

    public class Struct
    {
        public float number { get; set; }
        public string unit { get; set; }
    }

    public class Variation
    {
        public long id { get; set; }
        public float price { get; set; }
        public Attribute_Combinations[] attribute_combinations { get; set; }
        public int available_quantity { get; set; }
        public int sold_quantity { get; set; }
        public object[] sale_terms { get; set; }
        public string[] picture_ids { get; set; }
        public string catalog_product_id { get; set; }
    }

    public class Attribute_Combinations
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
        public object value_struct { get; set; }
        public Value2[] values { get; set; }
    }

    public class Value2
    {
        public string id { get; set; }
        public string name { get; set; }
        public object _struct { get; set; }
    }

}
