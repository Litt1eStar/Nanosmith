using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GeneratedItemObjectController : MonoBehaviour
{
    public GameObject selection;
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;

    private ItemsDataBean itemData;
    private ShopGenerateItemController shopController;
    public void Init(ItemsDataBean myData)
    {
        itemData = myData;
        if (itemData != null)
        {
            itemName.text = myData.itemName;
            itemPrice.text = myData.itemMarginNVC.ToString();
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemKey + "_icon");
            selection.SetActive(false);
        }
    }

    public void RegisterShopGeneratedController(ShopGenerateItemController shopCrtl)
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

        int x = 10;

        bool checkValueOfX = x < 10 ? true : false;

        if (x < 10)
        {
            print("x is less than 10");
        }
    }

    

    public void OnClickSelectedObject()
    {
        shopController.selectShopItemsObject(itemData);
    }

    public void OnClickSeeMoreInformation()
    {
        shopController.OnclickSeeMoreInformation(itemData);
    }
}
