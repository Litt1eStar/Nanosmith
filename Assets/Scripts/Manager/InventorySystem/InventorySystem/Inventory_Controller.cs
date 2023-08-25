using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Controller : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public GameObject gridLayoutGroup;
    public PlacementSystem placementSystem;
    public GameObject inventoryUiPanel;
    public int currentStack;

    private PlayerGameplayData playerGameplayData;
    public List<PlayerItemData> inventoryItemList = new List<PlayerItemData>();
    public PlayerItemData currentData;

    private HashSet<int> addedItemId = new HashSet<int>();

    private int openCounter = 0;

    private void Awake()
    {
        inventoryUiPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenInventory();
        }
    }

    public void ClosePanel()
    {
        inventoryUiPanel.SetActive(false);
    }
    public void OnClickSelectedObject(PlayerItemData myData)
    {
        if (myData != null)
        {
            currentData = myData;
            Debug.Log("OnclickSelectedObject currentData :: " + currentData.machineDataBean.machineName);
        }
        else
        {
            Debug.LogError($"myData is Null :: {myData}");
        }
    }

    public void OpenInventory()
    {
        inventoryUiPanel.SetActive(true);
    }

    public void GetItemData()
    {
        inventoryItemList.Clear();

        playerGameplayData = Main.PlayerManager.CreatePlayerGameplayData();

        playerGameplayData.inventory.gameItemListDict.Values.ToList().ForEach(i => {
            inventoryItemList.Add(i);
        });
    }
    public void AddItemToPanel()
    {
        GetItemData();
        Dictionary<int, int> itemStacks = new Dictionary<int, int>();
        foreach (PlayerItemData data in inventoryItemList)
        {
            if (data != null)
            {
                int itemID = int.Parse(data.machineDataBean.itemID);

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

            PlayerItemData data = inventoryItemList.Find(item => int.Parse(item.machineDataBean.itemID) == itemID);
            if (data != null)
            {
                GameObject go = GameObjectUtil.Instance.AddChild(gridLayoutGroup, inventoryItemPrefab);

                switch (data.itemType)
                {
                    case ItemType.Machine:
                        Inventory_ObjectController machineObj = go.GetComponent<Inventory_ObjectController>();
                        machineObj.Init(data, placementSystem);
                        machineObj.SetInventory(this);
                        string itemNameMachine = data.machineDataBean.itemKey;
                        machineObj.gameObject.name = itemNameMachine + "_prefab(InventoryPrefab)";
                        break;
                    case ItemType.PowerDevice:
                        Inventory_ObjectController powerObj = go.GetComponent<Inventory_ObjectController>();
                        powerObj.Init(data, placementSystem);
                        powerObj.SetInventory(this);
                        string itemNamePower = data.machineDataBean.itemKey;
                        powerObj.gameObject.name = itemNamePower + "_prefab(InventoryPrefab)";
                        break;
                    case ItemType.Storage:
                        Inventory_ObjectController storageObj = go.GetComponent<Inventory_ObjectController>();
                        storageObj.Init(data, placementSystem);
                        storageObj.SetInventory(this);
                        string itemNameStorage = data.machineDataBean.itemKey;
                        storageObj.gameObject.name = itemNameStorage + "_prefab(InventoryPrefab)";
                        break;
                    case ItemType.ResearchAndDevelopDevice:
                        Inventory_ObjectController rdObj = go.GetComponent<Inventory_ObjectController>();
                        rdObj.Init(data, placementSystem);
                        rdObj.SetInventory(this);
                        string itemNameRD = data.machineDataBean.itemKey;
                        rdObj.gameObject.name = itemNameRD + "_prefab(InventoryPrefab)";
                        break;
                    case ItemType.EnvironmentalControlDevice:
                        Inventory_ObjectController environmentControlObj = go.GetComponent<Inventory_ObjectController>();
                        environmentControlObj.Init(data, placementSystem);
                        environmentControlObj.SetInventory(this);
                        string itemNameENV = data.machineDataBean.itemKey;
                        environmentControlObj.gameObject.name = itemNameENV + "_prefab(InventoryPrefab)";
                        break;
                }

                // Set stack count in the UI
                //objController.UpdateStackCount(stackCount);
            }
        }



        /*inventoryItemList.ForEach(data => {
            if (data != null)
            {
                GameObject go = GameObjectUtil.Instance.AddChild(gridLayoutGroup, inventoryItemPrefab);
                switch (data.itemType)
                {
                    case ItemType.Machine:
                        Inventory_ObjectController machineObj = go.GetComponent<Inventory_ObjectController>();
                        machineObj.Init(data, placementSystem);
                        machineObj.SetInventory(this);
                        string itemName = data.machineDataBean.itemKey;
                        machineObj.gameObject.name = itemName + "_prefab(InventoryPrefab)";
                        break;
                    case ItemType.PowerDevice:
                        Inventory_ObjectController powerObj = go.GetComponent<Inventory_ObjectController>();
                        powerObj.Init(data, placementSystem);
                        powerObj.SetInventory(this);
                        break;
                    case ItemType.Storage:
                        Inventory_ObjectController storageObj = go.GetComponent<Inventory_ObjectController>();
                        storageObj.Init(data, placementSystem);
                        storageObj.SetInventory(this);
                        break;
                    case ItemType.ResearchAndDevelopDevice:
                        Inventory_ObjectController rdObj = go.GetComponent<Inventory_ObjectController>();
                        rdObj.Init(data, placementSystem);
                        rdObj.SetInventory(this);
                        break;
                    case ItemType.EnvironmentalControlDevice:
                        Inventory_ObjectController environmentControlObj = go.GetComponent<Inventory_ObjectController>();
                        environmentControlObj.Init(data, placementSystem);
                        environmentControlObj.SetInventory(this);
                        break;
                }
            } 
        });*/
    }

    public PlayerItemData InventoryData()
    {
        Debug.Log("InventoryData is SendToGrid");
        return currentData;
    }
}
