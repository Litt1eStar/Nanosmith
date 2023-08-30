using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputItem_ObjectController : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    private ItemsDataBean itemData;
    private MachineItemMenu_Controller machineMenuController;
    public void Init(ItemsDataBean currentPlayerItemData)
    {
        itemData = currentPlayerItemData;
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
        }
    }

    public void RegisterMachineMenuController(MachineItemMenu_Controller menuCrtl)
    {
        machineMenuController = menuCrtl;
    }
    public void OnClickSelected()
    {
        machineMenuController.OnClickSelectedOutput(itemData);
    }

    public void RemoveItemFromSlot()
    {
        machineMenuController.RemoveItemFromOutputSlot();
    }
}
