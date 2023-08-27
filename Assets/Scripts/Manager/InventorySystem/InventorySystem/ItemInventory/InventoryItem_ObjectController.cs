using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem_ObjectController : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI stack;
    private InventoryItem_Controller inventory;

    public PlayerItemData itemData;
    public List<PlayerItemData> playerInventoryItem = new List<PlayerItemData>();

    private void Awake()
    {
        inventory = GetComponent<InventoryItem_Controller>();
    }

    public void Init(PlayerItemData myData)
    {
        itemData = myData;
        Debug.Log("Item Data :: " + itemData.itemsDataBean.itemName);
        Debug.Log("ItemData Amount :: " + itemData.stack);
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemsDataBean.itemKey + "_icon");
            stack.text = itemData.stack.ToString();
            //selection.SetActive(false);
        }
    }

    public void OnClickSelectedObject()
    {
        if (itemData != null)
        {
            inventory.OnClickSelectedObject(itemData);
            //Debug.Log("ItemData send to StartPlacement :: " + itemData.itemsDataBean.itemName);
        }
        else
        {
            Debug.LogError("ItemData is NULL");
        }
    }

    public void SetInventory(InventoryItem_Controller inventoryController)
    {
        inventory = inventoryController;
    }
}
