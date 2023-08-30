using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSlot_ObjectController : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    private ItemsDataBean itemData;
    private MachineItemMenu_Controller _menuCtrl;
    private string _itemName;

    private void Start()
    {
        itemIcon = GetComponent<Image>();
    }
    public void Init(string itemName)
    {   
        _itemName = itemName;
        itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(_itemName.ToLower() + "_icon");
        var a_value = itemIcon.color;
        a_value.a = .85f;
        itemIcon.color = a_value;
    }

    public void RegisterMachineMenuController(MachineItemMenu_Controller menuCrtl)
    {
        _menuCtrl = menuCrtl;
    }

    public void Display()
    {
        Debug.Log($"recipe Name [{_itemName}]");
    }
}
