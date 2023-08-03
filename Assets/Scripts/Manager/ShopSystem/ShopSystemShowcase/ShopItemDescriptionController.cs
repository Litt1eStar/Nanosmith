using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItemDescriptionController : MonoBehaviour
{
    public GameObject itemDescriptionPanel;
    public GameObject itemGameResourceStatus;
    public GameObject itemCraftingPanel;
    public GameObject inItemDescriptionPanel;

    public Image itemIcon;
    public TextMeshProUGUI mainItemName;
    public TextMeshProUGUI mainItemPrice;

    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemGenre;
    public TextMeshProUGUI itemRarity;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemSeasonal;
    public TextMeshProUGUI productionRate;
    public TextMeshProUGUI itemDescription;

    public void OpenDescriptionPanel(ItemsDataBean targetData)
    {
        Debug.Log("OpenDescriptionPanel [target Data] :: [" + targetData + "]");
        if (targetData != null)
        {
            itemDescriptionPanel.SetActive(true);
            itemGameResourceStatus.SetActive(true);
            itemCraftingPanel.SetActive(true);
            inItemDescriptionPanel.SetActive(true);
            //itemNameUI    .text = targetData.itemName;
            itemPrice.text = targetData.itemMarginNVC.ToString();

            switch (targetData.itemType)
            {
                case ItemType.GameResourceItem:
                    GameResourceItemDataBean gameResourceData = (GameResourceItemDataBean)targetData;
                    mainItemName.text = gameResourceData.itemName;
                    mainItemPrice.text = "NVC$ : " + gameResourceData.itemMarginNVC.ToString();
                    itemNameUI.text = "Item Name = " + gameResourceData.itemName;
                    #region itemGenreRemoveNull
                    List<string> nonNullValueList = new List<string>();
                    foreach (string value in targetData.itemGenre)
                    {
                        if (value != null && value != "null")
                        {
                            nonNullValueList.Add(value);
                        }
                    }
                    string[] descriptionItemGenre = nonNullValueList.ToArray();
                    #endregion
                    itemGenre.text = "Item Genre : " + string.Join(", ", descriptionItemGenre);
                    itemRarity.text = "Item Rarity : " + gameResourceData.itemRarityDisplay;
                    itemPrice.text = "Item Price : " + gameResourceData.itemMarginNVC.ToString() + " NVC$";
                    itemSeasonal.text = "Item Seasonal : " + gameResourceData.itemSeasonalPopularity.ToString();
                    productionRate.text = "Production Rate/Minute : " + gameResourceData.itemProductionRatePerMinute.ToString();
                    break;
                default:
                    break;
            }

            itemDescription.text = targetData.itemDescription;
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(targetData.itemKey + "_icon");
        }
    }

    public void CloseDescriptionPanel()
    {
        itemDescriptionPanel.SetActive(false);
        itemGameResourceStatus.SetActive(false);
        itemCraftingPanel.SetActive(false);
        inItemDescriptionPanel.SetActive(false);
    }
}
