using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineItemMenu_Controller : MonoBehaviour
{
    [SerializeField] private InventoryItem_Controller inventoryItemController;
    public GameObject itemMenuPanel;
    public GameObject itemObjectListPrefab;
    public GameObject itemMenuGridLayout;

    public GameObject outputItemPrefab;
    public GameObject outputSlot;

    public delegate void SelectShopItemsObject(ItemsDataBean itemsDataBean);
    public SelectShopItemsObject selectShopItemsObject;

    private ItemsDataBean currentData;
    private ItemsDataBean itemData;

    public ResourceGenerate_Controlelr resourceGenerateCrtl;
    public ItemsDataBean outputData;
    public List<PlayerItemData> addedItem = new();
    public int openCounter = 0;


    private void Awake()
    {
        SpriteSheetUtil.Instance.Init();
    }

    private void Start()
    {
        itemMenuPanel.SetActive(false);
    }

    public void OnClickSelectedObject(ItemsDataBean itemsDataBean)
    {
        currentData = itemsDataBean;
        itemData = currentData;
        selectShopItemsObject?.Invoke(currentData);
    }

    public void AddItemToPanel()
    {
        if (openCounter == 0)
        {
            Debug.Log("AddItemToPanel()");
            List<ItemsDataBean> allItems = Main.InventoryManager.GetAllItemData();
            Debug.Log("OpenShop allItems :: " + allItems.Count + " | amount of GeneratedRequiredItem :: ");

            allItems.ForEach(data => {
                switch (data.itemType)
                {
                    case ItemType.GameResourceItem:
                        GameObject go = GameObjectUtil.Instance.AddChild(itemMenuGridLayout, itemObjectListPrefab);
                        Debug.Log("Item in Menu :: " + data.itemName + " Type :: " + data.itemType);
                        MachineItemMenu_ObjectController itemObj = go.GetComponent<MachineItemMenu_ObjectController>();
                        itemObj.Init(data);
                        itemObj.RegisterMachineMenuController(this);
                        break;
                    default: break;
                }
            });

            openCounter++;
        }
    }
    public void AddItemToOutputSlot(ItemsDataBean menuItemSelected)
    {
        outputData = menuItemSelected;
        if (outputData != null)
        {
            Debug.Log("AddItemToOutputSlot");
            GameObject go = GameObjectUtil.Instance.AddChild(outputSlot, outputItemPrefab);
            OutputItemData_ObjectController outputObj = go.GetComponent<OutputItemData_ObjectController>();
            outputObj.Init(outputData);
            outputObj.RegisterMachineMenuController(this);
            resourceGenerateCrtl.RecipeMapping(outputData);
        }
    }
    public void OnClickSelectedOutput(ItemsDataBean data)
    {
        outputData = data;
        Debug.Log("Current Data :: " + outputData.itemName);
    }
    
    public void RemoveItemFromOutputSlot()
    {
        GameObjectUtil.Instance.DestroyAllChildren(outputSlot);// Clear View
        resourceGenerateCrtl._currentInputItemList.Clear(); //
        resourceGenerateCrtl.listToCheck.Clear();
        Debug.Log("RemoveItemFromOutputSlot :: " + resourceGenerateCrtl._currentInputItemList.Count);
        outputData = null;
    }

    public bool isOutputSlotFree()
    {
        List<GameObject> outputChildren = new List<GameObject>();
        outputChildren = GameObjectUtil.Instance.GetChildren(outputSlot);
        int c_count = outputChildren.Count;

        if (c_count > 0 )
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    public ItemsDataBean OutputData()
    {
        return outputData;
    }
}
