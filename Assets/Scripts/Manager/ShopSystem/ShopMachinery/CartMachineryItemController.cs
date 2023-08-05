using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartMachineryItemController : MonoBehaviour
{
    public GameObject selection;
    public Image itemIcon;

    public MachineryDataBean itemData;
    private ShopCartController cartController;

    private string machinePriceFormat;
    public void Init(MachineryDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            selection.SetActive(false);
        }
    }

    public void OnClickSelectedObject()//*******
    {
        Debug.Log("OnClickSelectedObject[CartMachineryItemController] is ACTIVE");
        Debug.Log(this.name);
        cartController.OnClickSelectedObject(this);
    }
    public void RegisterShopMachineController(ShopCartController shopCrtl)
    {
        cartController = shopCrtl;
        //RegisterSelectDelegate(cartController.selectCartItemsObject);
        shopCrtl.selectCartItemObject += SelectedObject;
    }

    public void RegisterSelectDelegate(ShopCartController cartCtrl)
    {
        cartController = cartCtrl;
        cartCtrl.selectCartItemObject += SelectedObject;
    }
    public void SelectedObject(CartMachineryItemController itemInCart)
    {
        if (itemInCart == this)
        {
            selection.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
        }
    }
    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }


}
