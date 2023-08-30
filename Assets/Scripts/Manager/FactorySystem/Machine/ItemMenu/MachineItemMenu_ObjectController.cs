using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineItemMenu_ObjectController : MonoBehaviour
{
    public GameObject selection;
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public GameObject addButton;

    private ItemsDataBean itemData;
    private MachineItemMenu_Controller machineMenuController;

    private void Awake()
    {
        addButton.SetActive(false);
    }

    private void Update()
    {
        GetSlotInfo();
    }
    public void Init(ItemsDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemName.text = myData.itemName;
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            selection.SetActive(false);
        }  
    }

    public void RegisterMachineMenuController(MachineItemMenu_Controller menuCrtl)
    {
        machineMenuController = menuCrtl;
        menuCrtl.selectShopItemsObject += SelectedObject;
    }

    public void SelectedObject(ItemsDataBean itemsDataBean)
    {
        if (itemData == itemsDataBean)
        {
            selection.SetActive(true);
            addButton.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
            addButton.SetActive(false);
        }
    }

    public void OnClickSelectedObject()
    {
        machineMenuController.OnClickSelectedObject(itemData);
    }

    public void AddItemToOutputSlot()
    {
        if (GetSlotInfo())
        {
            machineMenuController.AddItemToOutputSlot(itemData);
        }
        else
        {
            Debug.Log("In slot there is item");
            return;
        }
    }

    public bool GetSlotInfo()
    {
        List<GameObject> p =  GameObjectUtil.Instance.GetChildren(machineMenuController.outputSlot);
        int p_count = p.Count;
        if (p_count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
