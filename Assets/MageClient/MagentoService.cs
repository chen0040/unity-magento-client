using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public delegate void CategoryDownloadCallback(Category category);
public delegate void CategoryProductDownloadCallback(long categoryId, List<CategoryProduct> cpList);
public delegate void ProductMediaListDownloadCallback(List<ProductMedia> pmList);
public delegate void ProductDownloadCallback(Product p);
public delegate void MagentoBinaryDownloadCallback(byte[] bytes);
public delegate void MagentoTextureDownloadCallback(Texture2D texture);
public delegate void StringListDownloadCallback(List<string> texts);

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
        WWW cases = new WWW(baseUrl + "/rest/V1/categories/" + categoryId + "/products");
        yield return cases;

        string json = cases.text;

        List<CategoryProduct> result = JsonConvert.DeserializeObject<List<CategoryProduct>>(json);

        callback(categoryId, result);
    }

    public IEnumerator DownloadProductDetail(string sku, ProductDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/products/" + EncodeUriComponent(sku));
        yield return cases;

        string json = cases.text;

        Product result = JsonConvert.DeserializeObject<Product>(json);
        callback(result);
    }

    public IEnumerator DownloadProductMediaList(string sku, ProductMediaListDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/products/" + EncodeUriComponent(sku) + "/media");
        yield return cases;

        string json = cases.text;

        List<ProductMedia> result = JsonConvert.DeserializeObject<List<ProductMedia>>(json);
        callback(result);
    }

    public IEnumerator DownloadProductImageUrlList(string sku, StringListDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/products/" + EncodeUriComponent(sku) + "/media");
        yield return cases;

        string json = cases.text;

        List<ProductMedia> mediaList = JsonConvert.DeserializeObject<List<ProductMedia>>(json);
        List<string> result = new List<string>();

        foreach (ProductMedia media in mediaList)
        {
            if (media.media_type == "image")
            {
                string url = baseUrl + "/pub/media/catalog/product/" + media.file;
                result.Add(url);
            }
        }


        callback(result);
    }

    public IEnumerable DownloadProductVideoUrlList(string sku, StringListDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/products/" + EncodeUriComponent(sku) + "/media");
        yield return cases;

        string json = cases.text;

        List<ProductMedia> mediaList = JsonConvert.DeserializeObject<List<ProductMedia>>(json);
        List<string> result = new List<string>();

        foreach (ProductMedia media in mediaList)
        {
            if (media.media_type == "video")
            {
                string url = baseUrl + "/pub/media/catalog/product/" + media.file;
                result.Add(url);
            }
        }


        callback(result);
    }

    public IEnumerator DownloadProductImages(string sku, MagentoBinaryDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/products/" + EncodeUriComponent(sku) + "/media");
        yield return cases;

        string json = cases.text;

        List<ProductMedia> mediaList = JsonConvert.DeserializeObject<List<ProductMedia>>(json);

        foreach (ProductMedia media in mediaList)
        {
            if (media.media_type == "image")
            {
                string url = baseUrl + "/pub/media/catalog/product/" + media.file;
                cases = new WWW(url);
                yield return cases;

                byte[] bytes = cases.bytes;

                callback(bytes);
            }
        }
    }

    public IEnumerator DownloadProductTextures(string sku, MagentoTextureDownloadCallback callback)
    {
        WWW cases = new WWW(baseUrl + "/rest/V1/products/" + EncodeUriComponent(sku) + "/media");
        yield return cases;

        string json = cases.text;

        List<ProductMedia> mediaList = JsonConvert.DeserializeObject<List<ProductMedia>>(json);

        foreach (ProductMedia media in mediaList)
        {
            if (media.media_type == "image")
            {
                string url = baseUrl + "/pub/media/catalog/product/" + media.file;
                cases = new WWW(url);
                yield return cases;

                callback(cases.texture);
            }
        }
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
