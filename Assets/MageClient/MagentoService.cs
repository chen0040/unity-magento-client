using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public delegate void CategoryDownloadCallback(Category category);
public delegate void CategoryProductDownloadCallback(long categoryId, List<CategoryProduct> cpList);
public delegate void ProductDownloadCallback(Product p);
public delegate void MagentoBinaryDownloadCallback(byte[] bytes);
public delegate void MagentoTextureDownloadCallback(Texture2D texture);

public class MagentoService : MonoBehaviour {

    private string baseUrl = "http://localhost";
    private static MagentoService mInstance = null;

	// Use this for initialization
	void Start () {
       
	}

    public void Initialize(string baseUrl)
    {
        this.baseUrl = baseUrl;
    }


    public IEnumerator DownloadRootCategory(CategoryDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/categories");
        yield return cases;
        
        string json = cases.text;

        Category result = JsonConvert.DeserializeObject<Category>(json);

        callback(result);
    }

    public IEnumerator DownloadProductsInCategory(long categoryId, CategoryProductDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "//magento/categories/" + categoryId + "/products");
        yield return cases;

        string json = cases.text;

        List<CategoryProduct> result = JsonConvert.DeserializeObject<List<CategoryProduct>>(json);

        callback(categoryId, result);
    }

    public IEnumerator DownloadProductDetail(string sku, ProductDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/magento/products/" + EncodeUriComponent(sku));
        yield return cases;

        string json = cases.text;

        Product result = JsonConvert.DeserializeObject<Product>(json);
        callback(result);
    }

    public IEnumerator DownloadProductImage(string sku, MagentoBinaryDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/product-img/" + EncodeUriComponent(sku));
        yield return cases;

        byte[] bytes = cases.bytes;

        callback(bytes);
    }

    public IEnumerator DownloadProductTexture(string sku, MagentoTextureDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/product-img/" + EncodeUriComponent(sku));
        yield return cases;

        callback(cases.texture);
    }

    public IEnumerator GetCategoryImage(long categoryId, MagentoBinaryDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/magento/categories/" + categoryId + "/icon");
        yield return cases;

        byte[] bytes = cases.bytes;

        callback(bytes);
    }

    public IEnumerator GetCategoryTexture(long categoryId, MagentoTextureDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/magento/categories/" + categoryId + "/icon");
        yield return cases;

        callback(cases.texture);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public string EncodeUriComponent(string component)
    {
        return WWW.EscapeURL(component).Replace("+", "%20");
    }

    void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
        }
        else
        {
            Debug.Log("Should not reach here");
        }
    }

    public static MagentoService Instance
    {
        get
        {
            if (!mInstance)
            {
                mInstance = GameObject.FindObjectOfType<MagentoService>();
            }
            return mInstance;
        }
    }


}
