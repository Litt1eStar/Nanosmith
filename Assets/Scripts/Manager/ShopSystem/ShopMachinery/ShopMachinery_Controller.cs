using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopMachinery_Controller : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject itemObjectListPrefab;
    public GameObject shopItemGridLayout;

    public ShopMachineryDesc_Controller shopMachineryDescriptionController;

    public CartMachinery_Controller shopCart_Controller;

    public delegate void SelectShopItemsObject(MachineryDataBean machineryData);
    public SelectShopItemsObject selectShopItemsObject;

    private MachineryDataBean currentData;
    public int openCounter = 0;

    public long playerWallet = 900000000000000000;

    private void Awake()
    {
        SpriteSheetUtil.Instance.Init();
    }

    private void Start()
    {
        ShopPanel.SetActive(false);
        shopMachineryDescriptionController.CloseDescriptionPanel();
    }

    private void Update()
    {
        if (openCounter == 0)
        {
            //Debug.Log("AddItemPanel is in this Method");
            AddItemToPanel();
            openCounter += 1;
        }

    }
    public void OnClickSelectedObject(MachineryDataBean itemsDataBean)
    {
        currentData = itemsDataBean;
        selectShopItemsObject?.Invoke(currentData);

        //Debug.Log("OnClickSelectedObject[ShopMachineryController] :: " + currentData);
        //Debug.Log("Selected Item ID : " + currentData.itemID + " | name : " + currentData.machineName);
    }
    
    public void OnclickSeeMoreInformation(MachineryDataBean itemsDataBean)
    {

        shopMachineryDescriptionController.OpenDescriptionPanel(itemsDataBean);
        Debug.Log("OnclickSeeMoreInformation is WORKING ");

    }

    public void ClearShop()
    {
        GameObjectUtil.Instance.DestroyAllChildren(shopItemGridLayout);
    }

    public void CloseShopPanel()
    {
        ShopPanel.SetActive(false);
        shopCart_Controller.ClearCart();
        Debug.Log("CloseShopPanel is WORKING :: ShopPanel Name ::" + ShopPanel.name);
        selectShopItemsObject = null;
    }

    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }


    public void AddItemToPanel()
    {
        List<MachineryDataBean> allItems = Main.MachineManager.GetAllItemData();
        Debug.Log("Machine allItems :: " + allItems.Count);
        allItems.ForEach(data =>
        {
            GameObject go = GameObjectUtil.Instance.AddChild(shopItemGridLayout, itemObjectListPrefab);
            switch (data.itemType)
            {
                case ItemType.Machine:
                    MachineryItem_ObjectController machineObj = go.GetComponent<MachineryItem_ObjectController>();
                    machineObj.Init(data);
                    machineObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.PowerDevice:        
                    MachineryItem_ObjectController powerObj = go.GetComponent<MachineryItem_ObjectController>();
                    powerObj.Init(data);
                    powerObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.Storage:
                    MachineryItem_ObjectController storageObj = go.GetComponent<MachineryItem_ObjectController>();
                    storageObj.Init(data);
                    storageObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.ResearchAndDevelopDevice:
                    MachineryItem_ObjectController rdObj = go.GetComponent<MachineryItem_ObjectController>();
                    rdObj.Init(data);
                    rdObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.EnvironmentalControlDevice:
                    MachineryItem_ObjectController environmentControlObj = go.GetComponent<MachineryItem_ObjectController>();
                    environmentControlObj.Init(data);
                    environmentControlObj.RegisterShopGeneratedController(this);
                    break;
            }
        });
    }

    public void AddToCart()
    {
        if (currentData != null)
        {
            shopCart_Controller.AddToCart(currentData, this);
        }
    }

    public void CheckOutItemInCart()
    {
        if (shopCart_Controller.totalPrice < playerWallet)
        {
            if (shopCart_Controller.CheckoutItemInCart().Count > 0)
            {
                //Debug.Log("CheckOutItemInCart Data :: " + shopCart_Controller.CheckoutItemInCart().Count);
                if (shopCart_Controller.CheckoutItemInCart() != null)
                {
                    Main.PlayerManager.AddPlayerInventory(shopCart_Controller.CheckoutItemInCart());
                    shopCart_Controller.ClearCart();
                }
                else
                {
                    Debug.Log("CheckOutItemInCart is ERROR");
                    return;
                }
            }
        }
        else
        {
            Debug.Log("You money is not enough");
        }
        //shopCart_Controller.CheckoutItemInCart();
    }

}
