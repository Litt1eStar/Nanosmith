using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_ObjectController : MonoBehaviour
{
    public Image itemIcon;
    private Inventory_Controller inventory;

    public PlayerItemData itemData;
    public List<PlayerItemData> playerInventoryItem = new List<PlayerItemData>();
    public PlacementSystem placementSystem;

    public void Init(PlayerItemData myData, PlacementSystem placementSys)
    {
        itemData = myData;
        placementSystem = placementSys;
        Debug.Log("ItemData Amount :: " + itemData.machineDataBean.machineName);
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.machineDataBean.itemKey + "_icon");
            //selection.SetActive(false);
        }
    }

    public void OnClickSelectedObject()
    {
        if (itemData != null)
        {
            inventory.OnClickSelectedObject(itemData);
            placementSystem.StartPlacement(itemData);
            Debug.Log("ItemData send to StartPlacement :: " + itemData.machineDataBean.machineName);
        }
        else
        {
            Debug.LogError("ItemData is NULL");
        }
    }

    public void SetInventory(Inventory_Controller inventoryController)
    {
        inventory = inventoryController;
    }
}
