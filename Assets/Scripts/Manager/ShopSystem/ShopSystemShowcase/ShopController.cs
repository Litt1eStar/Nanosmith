using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject itemObjectListPrefab;
    public GameObject shopItemGridLayout;
    public ShopItemDescriptionController shopItemDescriptionController;

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
        shopItemDescriptionController.CloseDescriptionPanel();
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
        
        shopItemDescriptionController.OpenDescriptionPanel(itemsDataBean);
        
        
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
        Debug.Log("OpenShop allItems :: " + allItems.Count);

        allItems.ForEach(data => {
            GameObject go = GameObjectUtil.Instance.AddChild(shopItemGridLayout, itemObjectListPrefab);
            ItemObjectController itemObj = go.GetComponent<ItemObjectController>();
            itemObj.Init(data);
            itemObj.RegisterShopController(this); // Moment that shop was opened, this command will tell that what method is used
        });
    }
}
