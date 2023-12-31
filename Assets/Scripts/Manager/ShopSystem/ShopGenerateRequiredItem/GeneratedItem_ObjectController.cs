using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GeneratedItem_ObjectController : MonoBehaviour
{
    public GameObject selection;
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;

    private ItemsDataBean itemData;
    private ShopGenerateItem_Controller shopController;
    public void Init(ItemsDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemName.text = myData.itemName;
            itemPrice.text = myData.itemMarginNVC.ToString() + " NVC$";
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            selection.SetActive(false);
        }
    }

    public void RegisterShopGeneratedController(ShopGenerateItem_Controller shopCrtl)
    {
        shopController = shopCrtl;
        shopCrtl.selectShopItemsObject += SelectedObject;
    }

    public void SelectedObject(ItemsDataBean itemsDataBean)
    {
        if (itemData == itemsDataBean)
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
        //shopController.selectShopItemsObject(itemData);
        //Debug.Log("OnClickSelectedObject[GeneratedItemObjectController] :: " + itemData);
    }

    public void OnClickSeeMoreInformation()
    {
        shopController.OnclickSeeMoreInformation(itemData);
    }
}