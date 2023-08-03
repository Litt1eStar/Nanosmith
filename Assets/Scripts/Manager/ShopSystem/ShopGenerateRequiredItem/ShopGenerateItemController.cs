using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopGenerateItemController : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject itemObjectListPrefab;
    public GameObject shopItemGridLayout;
    public ShopGeneratedItemDescriptionController shopGeneratedItemDescriptionController;

    public delegate void SelectShopItemsObject(ItemsDataBean itemsDataBean);
    public SelectShopItemsObject selectShopItemsObject;

    private ItemsDataBean currentData;
    public int openCounter = 0;
    private void Awake()
    {
        SpriteSheetUtil.Instance.Init();
    }

    private void Start()
    {
        ShopPanel.SetActive(false);
        shopGeneratedItemDescriptionController.CloseDescriptionPanel();
    }

    private void Update()
    {
        if (openCounter == 0)
        {
            AddItemToPanel();
            openCounter += 1;
        }
 
    }
    public void OnClickSelectedObject(ItemsDataBean itemsDataBean)
    {
        currentData = itemsDataBean;
        selectShopItemsObject?.Invoke(currentData);

        Debug.Log("Selected Item ID : " + currentData.itemID + " | name : " + currentData.itemName);
    }

    public void OnclickSeeMoreInformation(ItemsDataBean itemsDataBean)
    {

        shopGeneratedItemDescriptionController.OpenDescriptionPanel(itemsDataBean);
        Debug.Log("OnclickSeeMoreInformation is WORKING ");

    }

    public void CloseShopPanel()
    {
        ShopPanel.SetActive(false);
        Debug.Log("CloseShopPanel is WORKING :: ShopPanel Name ::"  + ShopPanel.name);
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
                GeneratedItemObjectController itemObj = go.GetComponent<GeneratedItemObjectController>();
                itemObj.Init(data);
                itemObj.RegisterShopGeneratedController(this); // Moment that shop was opened, this command will tell that what method is used
            }
        });


    }
}
