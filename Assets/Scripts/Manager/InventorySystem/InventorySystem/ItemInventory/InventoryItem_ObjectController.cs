using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class InventoryItem_ObjectController : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI stack;
    private InventoryItem_Controller inventory;
    private ResourceGenerate_Controlelr resourceGenerateCrtl;
    [SerializeField] private GameObject addButton;

    public PlayerItemData itemData;
    public List<PlayerItemData> playerInventoryItem = new List<PlayerItemData>();
    bool isButtonActive;
    private void Awake()
    {
        inventory = GetComponent<InventoryItem_Controller>();
        resourceGenerateCrtl =  GetComponent<ResourceGenerate_Controlelr>();
        addButton.SetActive(false);
    }

    public void Init(PlayerItemData myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemsDataBean.itemKey + "_icon");
            stack.text = itemData.stack.ToString();
            //selection.SetActive(false);
        }
    }
    
    public void AddItemToInputSlot()
    {
        Debug.Log("AddItemToInputSlot");
        inventory.AddItemToInputSlot(itemData); // item that init on inputSlot

    }

 
    public void OnClickSelectedObject()
    {
        if (itemData != null)
        {        
            inventory.OnClickSelectedObject(itemData);
            if (inventory.isMachinePanelOpening)
            {
                isButtonActive = !isButtonActive;
                addButton.SetActive(isButtonActive);
            }
            //Debug.Log("ItemData send to StartPlacement :: " + itemData.itemsDataBean.itemName);
        }
    }

    public void SetInventory(InventoryItem_Controller inventoryController)
    {
        inventory = inventoryController;
    }

    public void UpdateStackCount(int _stack)
    {
        stack.text = _stack.ToString();
    }
}
