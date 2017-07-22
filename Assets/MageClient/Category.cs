using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Category {
   public long id { get; set; }
   public long parent_id { get; set; }
   public string name { get; set; }
   public bool is_active { get; set; }
   public int position { get; set; }
   public int level { get; set; }
   public int product_count { get; set; }
   public List<Category> children_data { get; set; }
}
