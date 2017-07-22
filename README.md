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






