using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    [SerializeField] PlayerStatistics m_playerStats;

    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products

    //private string tempPurchase = "temp_purchase";
    //private string tempPurchase2 = "temp_purchase2";

    private string gems10 = "10_gems";
    private string gems50 = "50_gems";
    private string gems100 = "100_gems";
    private string gems500 = "500_gems";
    private string gems1000 = "1000_gems";
    private string gems5000 = "5000_gems";

    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable

        //builder.AddProduct(tempPurchase, ProductType.Consumable);
        //builder.AddProduct(tempPurchase2, ProductType.Consumable);

        builder.AddProduct(gems10, ProductType.Consumable);
        builder.AddProduct(gems50, ProductType.Consumable);
        builder.AddProduct(gems100, ProductType.Consumable);
        builder.AddProduct(gems500, ProductType.Consumable);
        builder.AddProduct(gems1000, ProductType.Consumable);
        builder.AddProduct(gems5000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void Gems10()
    {
        BuyProductID(gems10);
    }
    public void Gems50()
    {
        BuyProductID(gems50);
    }
    public void Gems100()
    {
        BuyProductID(gems100);
    }
    public void Gems500()
    {
        BuyProductID(gems500);
    }
    public void Gems1000()
    {
        BuyProductID(gems1000);
    }
    public void Gems5000()
    {
        BuyProductID(gems5000);
    }

    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, gems10, StringComparison.Ordinal))
        {
            Debug.Log("temp purchase");
            FindObjectOfType<PurchaseManager>().Gems10();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gems50, StringComparison.Ordinal))
        {
            Debug.Log("temp purchase");
            FindObjectOfType<PurchaseManager>().Gems50();
        }
        else if(String.Equals(args.purchasedProduct.definition.id, gems100, StringComparison.Ordinal))
        {
            Debug.Log("temp purchase");
            FindObjectOfType<PurchaseManager>().Gems100();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gems500, StringComparison.Ordinal))
        {
            Debug.Log("temp purchase");
            FindObjectOfType<PurchaseManager>().Gems500();
        }
        else if(String.Equals(args.purchasedProduct.definition.id, gems1000, StringComparison.Ordinal))
        {
            Debug.Log("temp purchase");
            FindObjectOfType<PurchaseManager>().Gems1000();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gems5000, StringComparison.Ordinal))
        {
            Debug.Log("temp purchase");
            FindObjectOfType<PurchaseManager>().Gems5000();
        }
        else
        {
            Debug.Log("Error");
        }
        return PurchaseProcessingResult.Complete;
    }

    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    //public void RestorePurchases()
    //{
    //    if (!IsInitialized())
    //    {
    //        Debug.Log("RestorePurchases FAIL. Not initialized.");
    //        FindObjectOfType<FrontPage>().restoreFailed();
    //        return;
    //    }

    //    if (Application.platform == RuntimePlatform.IPhonePlayer ||
    //        Application.platform == RuntimePlatform.OSXPlayer)
    //    {
    //        Debug.Log("RestorePurchases started ...");

    //        var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
    //        apple.RestoreTransactions((result) => {
    //            Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
    //        });
    //        FindObjectOfType<FrontPage>().restoreComplete();
    //    }
    //    else
    //    {
    //        Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    //        FindObjectOfType<FrontPage>().restoreFailed();
    //    }
    //}

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        //Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));

        if (product.definition.id == gems10)
        {
            FindObjectOfType<PurchaseManager>().PurchaseFailed();
        }
        else if (product.definition.id == gems50)
        {
            FindObjectOfType<PurchaseManager>().PurchaseFailed();
        }
        else if(product.definition.id == gems100)
        {
            FindObjectOfType<PurchaseManager>().PurchaseFailed();
        }
        else if (product.definition.id == gems500)
        {
            FindObjectOfType<PurchaseManager>().PurchaseFailed();
        }
        else if(product.definition.id == gems1000)
        {
            FindObjectOfType<PurchaseManager>().PurchaseFailed();
        }
        else if (product.definition.id == gems5000)
        {
            FindObjectOfType<PurchaseManager>().PurchaseFailed();
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
