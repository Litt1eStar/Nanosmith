using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryModelContainer inventoryModelContainer;

    private void Start()
    {
        InitInventoryData();
    }

    public List<ItemsDataBean> GetAllItemData()
    {
        return inventoryModelContainer.GetAllItems();
    }
    public void InitInventoryData()
    {
        StartCoroutine(LoadItemDataStreamingAsset("CSV_Data/Nanosmith_ResourceCSVData_27072023.csv"));
    }

    IEnumerator LoadItemDataStreamingAsset(string fileName)
    {
        string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        string result;
        if (filepath.Contains("://") || filepath.Contains(":///"))
        {
            WWW www = new WWW(filepath);
            yield return www;
            result = www.text;
        }
        else
        {
            //string filePathInStreamingAssets = System.IO.Path.Combine(Application.streamingAssetsPath, filepath);
            result = System.IO.File.ReadAllText(filepath);
        }

        //Debug.Log("Reading file from: " + filepath);
        //Debug.Log("Loaded File :: " + result);
        ReadItemDataFromCSV(result);


    }

    private void ReadItemDataFromCSV(string result)
    {
        string[] records = result.Split('\n');
        List<ItemsDataBean> allItemList = new List<ItemsDataBean>();

        foreach (string record in records)
        {
            string[] fields = record.Split(",");

            int itemID = int.Parse(fields[0]);
            string itemName = fields[1];

            #region itemGenre
            string[] itemGenre = new string[3]; // there is 3 object in itemGener
            itemGenre[0] = fields[2];
            itemGenre[1] = fields[3];
            itemGenre[2] = fields[4];
            if (itemGenre[0] == "null") { itemGenre[0] = null; }
            else if (itemGenre[1] == "null") { itemGenre[1] = null; }
            else if (itemGenre[2] == "null") { itemGenre[2] = null; }
            //Debug.Log("Item Genre :: " + itemGenre[0] + " | " + itemGenre[1] + " | " + itemGenre[2]);
            #endregion

            string itemKey = fields[5];
            UsedToType usedToType = (UsedToType)int.Parse(fields[6]);
            ItemType itemType = (ItemType)int.Parse(fields[7]);
            int itemRarity = int.Parse(fields[8]);
            string itemRarityDisplay = fields[9];
            float itemMarginNVC = float.Parse(fields[10]);

            #region itemRecipe
            string itemRecipeKey01 = fields[11] == "null" ? null : fields[11] ;
            int itemRecipeValue01 = int.Parse(fields[12]);

            string itemRecipeKey02 = fields[13] == "null" ? null : fields[13];
            int itemRecipeValue02 = int.Parse(fields[14]);

            string itemRecipeKey03 = fields[15] == "null" ? null : fields[15];
            int itemRecipeValue03 = int.Parse(fields[16]);
           
            //Debug.Log("itemRecipe :: [ItemKey01] " + itemRecipeKey01 + " + [ItemKey02] " + itemRecipeKey02 + " + [ItemKey03] " + itemRecipeKey03);
            #endregion
            string itemSeasonalPopularity = fields[17];
            int itemProductionRatePerMinute = int.Parse(fields[18]);
            string itemDescription = fields[19];
            ItemsDataBean item;
            //item = new ItemsDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemRecipe);
            switch (itemType)
            {
                // 0,1,2,3,4 => 0(GameResourceItemDataBean) 1,2,3(SpecialEventResourceDataBean) I will change it later for 1 , 2 , 3
                case ItemType.GameResourceItem:
                    item = new GameResourceItemDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarity, itemRarityDisplay, itemMarginNVC, itemProductionRatePerMinute, itemSeasonalPopularity, itemRecipeKey01, itemRecipeKey02, itemRecipeKey03, itemRecipeValue01, itemRecipeValue02, itemRecipeValue03);
                    break;
                case ItemType.NanoGameResourceItem:
                    item = new SpecialEventResourceItemDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarity, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemProductionRatePerMinute);
                    break;

                case ItemType.EenrgyItem:
                    item = new SpecialEventResourceItemDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarity, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemProductionRatePerMinute);
                    break;

                case ItemType.SyntheticItem:
                    item = new SpecialEventResourceItemDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarity, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemProductionRatePerMinute);
                    break;

                case ItemType.GenerateRequireItem:
                    item = new GenerateRequiredItemDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarity, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemProductionRatePerMinute);
                    break;

                default:
                    item = new ItemsDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarity, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemProductionRatePerMinute);
                    break;
            }
            //Debug.Log("item ItemDataBean created : " + item.itemID + " | " + item.itemName + " | itemMargin :: " + item.itemMarginNVC);
            allItemList.Add(item);
        }
        inventoryModelContainer.SetAllItem(allItemList);
    }
}