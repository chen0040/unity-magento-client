using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagentoServiceSample : MonoBehaviour {

	// Use this for initialization
	void Start () {

        MagentoService.Instance.Initialize("http://j-clef-web-01.japaneast.cloudapp.azure.com");
        
        StartCoroutine(MagentoService.Instance.DownloadRootCategory(rootCategory =>
        {
            List<Category> categories = rootCategory.children_data;
            foreach (Category c in categories)
            {
                Debug.Log("Downloaded category: " + c.name + " (id: " + c.id + ")");
            }

            
            DownloadProductsInCategory(categories[0].id);
        }));
	}

    private void DownloadProductsInCategory(long categoryId)
    {
        StartCoroutine(MagentoService.Instance.DownloadProductsInCategory(categoryId, (catId, products) => {
            for(int i=0; i < 6 && i < products.Count; ++i)
            {
                CategoryProduct categoryProduct = products[i];
                Debug.Log("Download products in category " + categoryId + ": " + categoryProduct.sku);
                DownloadProductDetail(categoryProduct.sku);
                if (i == 5)
                {
                    /*
                    DownloadProductImage(categoryProduct.sku);
                    DownloadProductTexture(categoryProduct.sku);*/
                }
            }
            
        }));
    }

    private void DownloadProductDetail(string sku)
    {
        StartCoroutine(MagentoService.Instance.DownloadProductDetail(sku, (product) => {
            Debug.Log("Name for sku " + product.sku + ": " + product.name);
            Debug.Log("Price for sku " + product.sku + ": " + product.price);
            Debug.Log("Weight for sku " + product.sku + ": " + product.weight);
        }));
    }

    private void DownloadProductImage(string sku)
    {
        StartCoroutine(MagentoService.Instance.DownloadProductImage(sku, (bytes) => {
            Debug.Log("Bytes for product " + sku + " is " + bytes.Length);
        }));
    }

    private void DownloadProductTexture(string sku)
    {
        StartCoroutine(MagentoService.Instance.DownloadProductTexture(sku, (texture) =>
        {
            Debug.Log("texture for product " + sku + " is " + (texture != null));
        }));
    }

	// Update is called once per frame
	void Update () {
	
	}
}
