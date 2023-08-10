using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CartGenerateItemController : MonoBehaviour
{
    public Image itemIcon;

    public ItemsDataBean itemData;

    private ShopItemCartController itemCartController;

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

    public void RegisterShopItemController(ShopItemCartController cartCrtl)
    {
        itemCartController = cartCrtl;
        //RegisterSelectDelegate(cartController.selectCartItemsObject);
        cartCrtl.selectCartItemObject += SelectedObject;
    }

    public void RegisterSelectDelegate(ShopItemCartController cartCtrl)
    {
        itemCartController = cartCtrl;
        cartCtrl.selectCartItemObject += SelectedObject;
    }
    public void SelectedObject(CartGenerateItemController itemInCart)
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
