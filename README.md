# unity-magento-client

Magento client implemented in Unity3D

# Install

Git clone this project and export it as a unity package, then import the package into your own project.

# Usage

Before calling any magento api, the base url of the magento site must be initialized:

### Initialization 
```cs
MagentoService.Instance.Initialize("http://www.your-magento.com");
```

### List Categories
The sample code below shows how to download the categories from the magento site:

```cs
StartCoroutine(MagentoService.Instance.DownloadRootCategory(rootCategory =>
	{
		List<Category> categories = rootCategory.children_data;
		foreach (Category c in categories)
		{
			Debug.Log("Downloaded category: " + c.name + " (id: " + c.id + ")");
			// do something else with each category here.
		}
	}
);
```

### List Product Summary under a Category

The sample code below shows how to download list all product summaries under a particular category 

```cs
long categoryId = 10;
StartCoroutine(MagentoService.Instance.DownloadProductsInCategory(categoryId, (catId, products) => {
	for(int i=0; i < 6 && i < products.Count; ++i)
	{
		CategoryProduct categoryProduct = products[i];
		Debug.Log("Download products in category " + categoryId + ": " + categoryProduct.sku);
		// use the sku to retrieve the product detail here 
	}
);
```

### Obtain detail of a product given its SKU

The sample code below shows to download the product detail associated with a particular sku 

```cs
string sku = "product_dynamic_17";
StartCoroutine(MagentoService.Instance.DownloadProductDetail(sku, (product) => {
	Debug.Log("Name for sku " + product.sku + ": " + product.name);
	Debug.Log("Price for sku " + product.sku + ": " + product.price);
	Debug.Log("Weight for sku " + product.sku + ": " + product.weight);
}));
```

### Obtain list of medias under the product (images and videos)

The sample code below shows how to download the media list under a product using its sku

```cs
StartCoroutine(MagentoService.Instance.DownloadProductMediaList(sku, (mediaList) => {
	foreach(ProductMedia media in mediaList){
		Debug.Log("type: " + media.media_type + "\tfilename: " + media.file);
	}
}));
```

If you prefer to get the list of image urls instead of the ProductMedia List as given by the above api call, the sample code below shows how to obtains urls of all images associated with a product using its sku 

```cs

```





