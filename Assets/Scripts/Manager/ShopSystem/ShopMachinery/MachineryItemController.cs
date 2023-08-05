using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class MachineryItemController : MonoBehaviour
{
    public GameObject selection;
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;

    private MachineryDataBean itemData;
    private ShopMachineryController shopController;

    public string machinePriceFormat;
    public void Init(MachineryDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemName.text = myData.machineName;
            itemPrice.text = FormatPrice(myData.machinePriceNVC) + " NVC$";
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            selection.SetActive(false);
        }
    }

    public void RegisterShopGeneratedController(ShopMachineryController shopCrtl)
    {
        shopController = shopCrtl;
        //RegisterSelectDelegate(shopController.selectShopItemsObject);
        shopCrtl.selectShopItemsObject += SelectedObject;
    }

    public void RegisterSelectDelegate(ShopMachineryController.SelectShopItemsObject delegateMethods)
    {
        delegateMethods += SelectedObject;
    }

    public void SelectedObject(MachineryDataBean machineryDataBean)
    {
        if (itemData == machineryDataBean)
        {
            selection.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
        }
    }

    public void OnClickSelectedObject()
    {
        shopController.OnClickSelectedObject(itemData);
    }

    public void OnClickSeeMoreInformation()
    {
        shopController.OnclickSeeMoreInformation(itemData);
    }

    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }


}
