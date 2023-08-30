using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CartGenerateItem_ObjectController : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI stackCountText;
    public ItemsDataBean itemData;

    private CartGenerateItem_Controller itemCartController;
    public int stackCount;
    private string machinePriceFormat;
    public void InitItemShop(ItemsDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            //selection.SetActive(false);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        stackCountText.text = stackCount.ToString();
    }

    public void IncreaseStack()
    {
        stackCount++;
        UpdateUI();
    }

    public void RemoveFromCart()
    {
        if (stackCount > 1) // If stack count is more than 1, just decrease the stack count
        {
            stackCount--;
            UpdateUI();
        }
        else // If stack count is 1, remove the item from the cart
        {
            itemCartController.RemoveItemFromCart(this);
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
