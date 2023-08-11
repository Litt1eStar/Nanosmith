using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CartGenerateItem_ObjectController : MonoBehaviour
{
    public Image itemIcon;

    public ItemsDataBean itemData;

    private CartGenerateItem_Controller itemCartController;

    private string machinePriceFormat;
    public void InitItemShop(ItemsDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            //selection.SetActive(false);
        }
    }

    public void OnClickSelectedObject()//*******
    {
        Debug.Log("OnClickSelectedObject[CartMachineryItemController] is ACTIVE");
        Debug.Log(this.name);
        itemCartController.OnClickSelectedObject(this);
    }

    public void RegisterShopItemController(CartGenerateItem_Controller cartCrtl)
    {
        itemCartController = cartCrtl;
        //RegisterSelectDelegate(cartController.selectCartItemsObject);
        cartCrtl.selectCartItemObject += SelectedObject;
    }

    public void RegisterSelectDelegate(CartGenerateItem_Controller cartCtrl)
    {
        itemCartController = cartCtrl;
        cartCtrl.selectCartItemObject += SelectedObject;
    }
    public void SelectedObject(CartGenerateItem_ObjectController itemInCart)
    {
        if (itemInCart == this)
        {
            //selection.SetActive(true);
        }
        else
        {
            //selection.SetActive(false);
        }
    }
    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }


}
