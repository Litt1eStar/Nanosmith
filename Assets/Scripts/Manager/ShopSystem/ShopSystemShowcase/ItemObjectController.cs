using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemObjectController : MonoBehaviour
{
    public GameObject selection;
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemPrice;

    private ItemsDataBean itemData;
    private ShopController shopController;
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

    public void RegisterShopController(ShopController shopCrtl)
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
        shopController.selectShopItemsObject(itemData);
    }

    public void OnClickSeeMoreInformation()
    {
        shopController.OnclickSeeMoreInformation(itemData);
    }
}
