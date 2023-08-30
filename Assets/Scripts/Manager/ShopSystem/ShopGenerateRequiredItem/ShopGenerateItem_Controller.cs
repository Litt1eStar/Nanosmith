using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ShopGenerateItem_Controller : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject itemObjectListPrefab;
    public GameObject shopItemGridLayout;
    public ShopGenerateItemDesc_Controller shopGeneratedItemDescriptionController;

    public Slider buyAmountSlider;
    public TextMeshProUGUI buyAmountText;

    public delegate void SelectShopItemsObject(ItemsDataBean itemsDataBean);
    public SelectShopItemsObject selectShopItemsObject;

    public CartGenerateItem_Controller shopItemCart_Controller;

    private ItemsDataBean currentData;
    private ItemsDataBean itemData;

    public int openCounter = 0;

    public long playerWallet = 900000000000000000;


    private void Awake()
    {
        SpriteSheetUtil.Instance.Init();
    }

    private void Start()
    {
        ShopPanel.SetActive(false);
        shopGeneratedItemDescriptionController.CloseDescriptionPanel();

        buyAmountSlider.onValueChanged.AddListener(UpdateBuyAmountText);
        UpdateBuyAmountText(1);
    }

    private void UpdateBuyAmountText(float value)
    {
        int buyAmount = Mathf.RoundToInt(value); // Round the float value to an integer
        buyAmountText.text = "Buy Amount: " + buyAmount;
    }

    private void Update()
    {
        if (openCounter == 0)
        {
            AddItemToPanel();
            openCounter += 1;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Main.PlayerManager.playerInventory.InfoPlayerInventoryTextConsole();
        }

    }

    public void AddToCart()
    {
        int buyAmount = Mathf.RoundToInt(buyAmountSlider.value);
        Debug.Log("currentData :: " + itemData); // null
        if (itemData != null)
        {
            for (int i = 0; i < buyAmount; i++)
            {
                shopItemCart_Controller.AddToCart(itemData, this);
            }
            //Debug.Log("Data :: " + currentData + " | ShopCartController :: " + this);
        }
    }

    public void CheckOutItemInCart()
    {
        if (shopItemCart_Controller.totalPrice < playerWallet)
        {
            if (shopItemCart_Controller.CheckoutItemInCart().Count > 0)
            {
                //Debug.Log("CheckOutItemInCart Data :: " + shopCart_Controller.CheckoutItemInCart().Count);
                if (shopItemCart_Controller.CheckoutItemInCart() != null)
                {
                    Main.PlayerManager.AddPlayerInventoryFromItemShop(shopItemCart_Controller.CheckoutItemInCart());
                    shopItemCart_Controller.ClearCart();
                }
                else
                {
                    Debug.Log("CheckOutItemInCart is ERROR");
                    return;
                }
            }
        }
    }
    public void OnClickSelectedObject(ItemsDataBean itemsDataBean)
    {
        currentData = itemsDataBean;
        itemData = currentData;
        //Debug.Log("currentData[OnClickSelectedObject] :: " + currentData);
        selectShopItemsObject?.Invoke(currentData);

        //Debug.Log("Selected Item ID : " + currentData.itemID + " | name : " + currentData.itemName);
    }

    public void OnclickSeeMoreInformation(ItemsDataBean itemsDataBean)
    {

        shopGeneratedItemDescriptionController.OpenDescriptionPanel(itemsDataBean);
        Debug.Log("OnclickSeeMoreInformation is WORKING ");

    }

    public void CloseShopPanel()
    {
        ShopPanel.SetActive(false);
        Debug.Log("CloseShopPanel is WORKING :: ShopPanel Name ::" + ShopPanel.name);
    }
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }

    public void AddItemToPanel()
    {
        List<ItemsDataBean> allItems = Main.InventoryManager.GetAllItemData();
        Debug.Log("OpenShop allItems :: " + allItems.Count + " | amount of GeneratedRequiredItem :: ");

        allItems.ForEach(data => {
            if (data.itemType == ItemType.GenerateRequireItem)
            {
                GameObject go = GameObjectUtil.Instance.AddChild(shopItemGridLayout, itemObjectListPrefab);
                Debug.Log("Item in ShopGRI :: " + data.itemName + " Type :: " + data.itemType);
                GeneratedItem_ObjectController itemObj = go.GetComponent<GeneratedItem_ObjectController>();
                itemObj.Init(data);
                itemObj.RegisterShopGeneratedController(this); // Moment that shop was opened, this command will tell that what method is used
            }
        });


    }
}