using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputItemPreview_ObjectController : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    private int slotIndex;

    private ItemsDataBean itemData;
    private MachineItemMenu_Controller _menuCtrl;
    private ResourceGenerate_Controlelr resourceGenerateCtrl;
    private PlayerItemData currentData;
    private string _itemName;
    public delegate void OnItemChanged(PlayerItemData item);
    public event OnItemChanged onItemChanged;
        
    private void Start()
    {
        itemIcon = GetComponent<Image>();
    }
    public void Init(string itemName, ResourceGenerate_Controlelr resourceCtrl, int index)
    {   
        _itemName = itemName;
        itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(_itemName.ToLower() + "_icon");
        var a_value = itemIcon.color;
        a_value.a = .85f;
        itemIcon.color = a_value;

        resourceGenerateCtrl = resourceCtrl;
        slotIndex = index;

    }

    public void RegisterResourceGenerateController(ResourceGenerate_Controlelr resourceCrtl)
    {
        resourceGenerateCtrl = resourceCrtl;
    }

    public void OnClickSelectedObject() // Contain Data of item & Data of Slot
    {
        Debug.Log("OnClickSelectedObject");
        itemData = resourceGenerateCtrl.MatchData(_itemName); // Data that already mapping to ItemsDataBean by using itemName
        resourceGenerateCtrl.HandleSlotClick(slotIndex);
        resourceGenerateCtrl.OpenPlayerInventory(itemData);
    }

    public void UpdateItem(PlayerItemData newItem)
    {
        currentData = newItem;

        // Notify subscribers that the item has changed
        if (onItemChanged != null)
        {
            onItemChanged(currentData);
        }
    }
}
