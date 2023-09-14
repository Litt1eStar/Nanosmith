using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItem_Controller : MonoBehaviour
{   public GameObject inventoryItemPrefab;
    public GameObject gridLayoutGroup;
    public GameObject inventoryUiPanel;
    public int currentStack;

    private PlayerGameplayData playerGameplayData;
    public List<PlayerItemData> inventoryItemList = new List<PlayerItemData>();
    public PlayerItemData currentData;
    public PlayerItemData currentDataOnMachinePanel;

    public ResourceGenerate_Controlelr resourceGenerateCrtl;
    public bool isMachinePanelOpening;

    public delegate void SelectItemObject(InventoryItem_ObjectController itemInInventoryObject);
    public SelectItemObject selectItemObject;

    public List<PlayerItemData> itemInInputSlotList = new List<PlayerItemData>();
    public enum inventoryState
    {
        onMachinePanel = 0,
        offMachinePanel = 1
    }
    private void Awake()
    {
        //inventoryUiPanel.SetActive(false);
        isMachinePanelOpening = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUiPanel.SetActive(true);
        }
    }
    public void OnClickSelectedObject(PlayerItemData myData)
    {
        if (myData != null)
        {
            if (!resourceGenerateCrtl.MachinePanelIsOpen())
            {
                currentData = myData;
                Debug.Log($"OnclickSelectedObject currentData :: {currentData.itemsDataBean.itemName} | State :: [MachinePanel Not Open]");
                Debug.Log("OnclickSelectedObject currentData Stack :: " + currentData.stack);
            }
            else
            {
                currentDataOnMachinePanel = myData;
                Debug.Log($"OnclickSelectedObject currentData :: {currentDataOnMachinePanel.itemsDataBean.itemName} | State :: [MachinePanel Open]");
                Debug.Log("OnclickSelectedObject currentData Stack :: " + currentDataOnMachinePanel.stack);
            }
        }
        else
        {
            Debug.LogError($"myData is Null :: {myData}");
        }
    }
    public PlayerItemData playerItemUseToGenerate()
    {
        PlayerItemData dataSelectedOnInv = currentData;
        return dataSelectedOnInv;
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
        GetItemData();

        Dictionary<int, int> itemStacks = new Dictionary<int, int>();
        foreach (PlayerItemData data in inventoryItemList)
        {
            if (data != null)
            {
                int itemID = (data.itemsDataBean.itemID);

                if (itemStacks.ContainsKey(itemID))
                {
                    itemStacks[itemID] += data.stack; // Increment stack count
                }
                else
                {
                    itemStacks[itemID] = data.stack; // Initialize stack count
                }
                Debug.Log($"ItemData[{data.itemsDataBean.itemName}] | {itemStacks[itemID]}");
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
                switch (data.itemType)
                {
                    case ItemType.GameResourceItem:
                       
                        InventoryItem_ObjectController gameResourceItem = go.GetComponent<InventoryItem_ObjectController>();
                        gameResourceItem.Init(data);
                        gameResourceItem.SetInventory(this);
                        string itemNameGameResource= data.itemsDataBean.itemKey;
                        gameResourceItem.gameObject.name = itemNameGameResource + "_prefab(InventoryPrefab)";
                        gameResourceItem.UpdateStackCount(currentStack);
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
                        generateRequireItem.UpdateStackCount(currentStack);
                        break;
                }
                if (data == null)
                {
                    Debug.Log(go.name);
                    return;
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

    public List<PlayerItemData> playerItemInInputSlot()
    {
        return itemInInputSlotList;
    }

    public void AddItemToInputSlot(PlayerItemData selectedItem)
    {
        GameObject targetSlot = resourceGenerateCrtl._selectedSlotInput;
        bool removed = Main.PlayerManager.playerInventory.allItemResourceDict.Values.ToList().Remove(selectedItem);
        if (removed)
        {
            // Item successfully removed from the inventory, now add it to the input slot.
            resourceGenerateCrtl.AddItemToInputSlot(selectedItem, targetSlot);
            itemInInputSlotList.Add(selectedItem);
        }
        else
        {
            Debug.LogError("Failed to remove item from inventory.");
        }
        /*resourceGenerateCrtl.AddItemToInputSlot(selectedItem, targetSlot);
        itemInInputSlotList.Add(selectedItem);*/
    }
    public void AddSelectedItemToPanel(ItemsDataBean itemOnItemInventory)
    {
        List<PlayerItemData> inventoryItemList = new List<PlayerItemData>();
        inventoryItemList = InventoryDataList();
        PlayerItemData targetItem = inventoryItemList.FirstOrDefault(item => item.itemsDataBean.itemID == itemOnItemInventory.itemID);
        //Debug.Log($"TargetItem :: {targetItem.itemsDataBean.itemName} | Data Type => {targetItem.itemsDataBean.itemType}");
        if (itemOnItemInventory != null)
        {
            GameObject go = GameObjectUtil.Instance.AddChild(gridLayoutGroup, inventoryItemPrefab);
            InventoryItem_ObjectController inventoryItem = go.GetComponent<InventoryItem_ObjectController>();
            inventoryItem.Init(targetItem);
            inventoryItem.SetInventory(this);
            // Update UI elements in the inventory panel with selected item's data.
          
        }
        else
        {
            Debug.LogWarning("GameObject for inventory item UI is null.");
            return;
        }

    }

    public PlayerItemData GetItemByItemData(ItemsDataBean itemData)
    {
        return inventoryItemList.Find(item => item.itemsDataBean == itemData);
    }

    public void AddItemToInventory(PlayerItemData newItem)
    {
        // Check if an item with the same ItemsDataBean already exists in the inventory
        PlayerItemData existingItem = GetItemByItemData(newItem.itemsDataBean);

        if (existingItem != null)
        {
            // If the item already exists, increase its stack count
            existingItem.stack += newItem.stack;
        }
        else
        {
            // If the item doesn't exist, add it to the inventory
            inventoryItemList.Add(newItem);
        }
    }
    public void ClearInventory()
    {
        GameObjectUtil.Instance.DestroyAllChildren(gridLayoutGroup);
    }
    public PlayerItemData InventoryItemData()
    {
        Debug.Log("InventoryData is SendToGrid");
        return currentData;
    }

    public List<PlayerItemData> InventoryDataList()
    {
        return inventoryItemList;
    }
}
