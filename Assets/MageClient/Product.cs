using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Product {

    public long id { get; set; }
    public string sku { get; set; }
    public string name { get; set; }
    public long attribute_set_id { get; set; }
    public double price { get; set; }
    public int status { get; set; }
    public int visibility { get; set; }
    public string type_id { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public double weight { get; set; }
    public List<MagentoAttribute> extension_attributes { get; set; }
    public List<string> product_links { get; set; }
    public List<double> tier_prices { get; set; }
    public List<MagentoAttribute> custom_attributes { get; set; }
}
