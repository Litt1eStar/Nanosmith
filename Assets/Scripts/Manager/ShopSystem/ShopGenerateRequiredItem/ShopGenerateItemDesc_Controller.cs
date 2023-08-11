using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ShopGenerateItemDesc_Controller : MonoBehaviour
{
    public GameObject itemDescriptionPanel;
    public GameObject itemGameResourceStatus;
    public GameObject inItemDescriptionPanel;

    public Image itemIcon;

    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemGenre;
    public TextMeshProUGUI itemRarity;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI productionRate;
    public TextMeshProUGUI itemDescription;

    public void OpenDescriptionPanel(ItemsDataBean targetData)
    {
        Debug.Log("OpenDescriptionPanel [target Data] :: [" + targetData + "]");
        if (targetData != null)
        {
            itemDescriptionPanel.SetActive(true);
            itemGameResourceStatus.SetActive(true);
            inItemDescriptionPanel.SetActive(true);
          
            switch (targetData.itemType)
            {
                case ItemType.GenerateRequireItem:
                    GenerateRequiredItemDataBean gameResourceData = (GenerateRequiredItemDataBean)targetData;

                    itemNameUI.text = "Item Name : " + gameResourceData.itemName;
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

        inItemDescriptionPanel.SetActive(false);
    }
}
