using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductMedia {

	public long id { get; set; }
   public string media_type { get; set; }
   public string label { get; set; }
   public int position { get; set; }
   public bool disabled { get; set;}
   public List<string> types { get; set; }
   public string file { get; set; }
}
