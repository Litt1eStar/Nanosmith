using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItem_Controller : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public GameObject gridLayoutGroup;
    public GameObject inventoryUiPanel;
    public int currentStack;

    private PlayerGameplayData playerGameplayData;
    public List<PlayerItemData> inventoryItemList = new List<PlayerItemData>();
    public PlayerItemData currentData;

    private void Awake()
    {
        //inventoryUiPanel.SetActive(false);
    }

    public void OnClickSelectedObject(PlayerItemData myData)
    {
        if (myData != null)
        {
            currentData = myData;
            Debug.Log("OnclickSelectedObject currentData :: " + currentData.itemsDataBean.itemName);
        }
        else
        {
            Debug.LogError($"myData is Null :: {myData}");
        }
    }

    public void GetItemData()
    {
        Debug.Log("GetItemData is Working");
        inventoryItemList.Clear();

        playerGameplayData = Main.PlayerManager.CreatePlayerGameplayDataItem();
        if (playerGameplayData != null)
        {
            Debug.Log("playerGameplayData is not Null | Amount of Data :: " + playerGameplayData.inventory.gameItemListDict.Values.Count);
            playerGameplayData.inventory.gameItemListDict.Values.ToList().ForEach(i =>
            {
                Debug.Log("Loop is Working");
                inventoryItemList.Add(i);
                Debug.Log("[GetItemData] :: " + i.itemsDataBean.itemName);
            });
            Debug.Log("OUT OF LOOP");
        }
        else { Debug.LogError("playerGameplayData is NULL"); }
               
    }

    public void AddItemToPanel()
    {
        Debug.Log("AddItemToPanel is Working");
        GetItemData();
        if (inventoryItemList != null)
        {
            Debug.Log("inventoryItemList Amount :: " + inventoryItemList.Count);
            Debug.LogError("inventoryItemList is not null");
        }
        else { Debug.LogError("inventoryItemList is null"); }
        inventoryItemList.ForEach(i => { Debug.Log("Item Name :: " + i.itemsDataBean.itemName); });
        Dictionary<int, int> itemStacks = new Dictionary<int, int>();
        foreach (PlayerItemData data in inventoryItemList)
        {
            if (data != null)
            {
                int itemID = (data.itemsDataBean.itemID);

                if (itemStacks.ContainsKey(itemID))
                {
                    itemStacks[itemID] += 1; // Increment stack count
                }
                else
                {
                    itemStacks[itemID] = 1; // Initialize stack count
                }
            }
        }

        foreach (KeyValuePair<int, int> kvp in itemStacks)
        {
            int itemID = kvp.Key;
            int stackCount = kvp.Value;
            currentStack = stackCount;

            PlayerItemData data = inventoryItemList.Find(item => item.itemsDataBean.itemID == itemID);
            if (data != null)
            {
                GameObject go = GameObjectUtil.Instance.AddChild(gridLayoutGroup, inventoryItemPrefab);
                if (go != null)
                {
                    Debug.LogError("go is not null");
                }
                switch (data.itemType)
                {
                    case ItemType.GameResourceItem:
                       
                        InventoryItem_ObjectController gameResourceItem = go.GetComponent<InventoryItem_ObjectController>();
                        gameResourceItem.Init(data);
                        gameResourceItem.SetInventory(this);
                        string itemNameGameResource= data.itemsDataBean.itemKey;
                        gameResourceItem.gameObject.name = itemNameGameResource + "_prefab(InventoryPrefab)";
                        break;

                    case ItemType.NanoGameResourceItem:
                        InventoryItem_ObjectController nanoResourceItem = go.GetComponent<InventoryItem_ObjectController>();
                        nanoResourceItem.Init(data);
                        nanoResourceItem.SetInventory(this);
                        string itemNameNano = data.itemsDataBean.itemKey;
                        nanoResourceItem.gameObject.name = itemNameNano + "_prefab(InventoryPrefab)";
                        break;

                    case ItemType.EenrgyItem:
                        InventoryItem_ObjectController energyItem = go.GetComponent<InventoryItem_ObjectController>();
                        energyItem.Init(data);
                        energyItem.SetInventory(this);
                        string itemNameEnergy = data.itemsDataBean.itemKey;
                        energyItem.gameObject.name = itemNameEnergy + "_prefab(InventoryPrefab)";
                        break;

                    case ItemType.SyntheticItem:
                        InventoryItem_ObjectController syntheticItem = go.GetComponent<InventoryItem_ObjectController>();
                        syntheticItem.Init(data);
                        syntheticItem.SetInventory(this);
                        string itemNameSynthetic = data.itemsDataBean.itemKey;
                        syntheticItem.gameObject.name = itemNameSynthetic + "_prefab(InventoryPrefab)";
                        break;

                    case ItemType.GenerateRequireItem:
                        Debug.Log("Game Resource :: " + data.itemsDataBean.itemName);
                        InventoryItem_ObjectController generateRequireItem = go.GetComponent<InventoryItem_ObjectController>();
                        generateRequireItem.Init(data);
                        generateRequireItem.SetInventory(this);
                        Debug.Log("Data :: " + data.itemsDataBean.itemName);
                        string itemNameENV = data.itemsDataBean.itemKey;
                        generateRequireItem.gameObject.name = itemNameENV + "_prefab(InventoryPrefab)";
                        break;
                }

                
                // Set stack count in the UI
                //objController.UpdateStackCount(stackCount);
            }
            else
            {
                Debug.LogError($"Data is Null {data}");
            }
        }
    }

    public PlayerItemData InventoryData()
    {
        Debug.Log("InventoryData is SendToGrid");
        return currentData;
    }
}
