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
    public void InitInventoryData()
    {
        StartCoroutine(LoadItemDataStreamingAsset("CSV_Data/Nanosmith_GameItemData_23072023.csv"));
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

        Debug.Log("Reading file from: " + filepath);
        Debug.Log("Loaded File :: " + result);
        ReadItemDataFromCSV(result);


    }

    private void ReadItemDataFromCSV(string result)
    {
        string[] records = result.Split('\n');
        int counter = 0;
        List<ItemsDataBean> allItemList = new List<ItemsDataBean>();

        foreach (string record in records)
        {
            counter++;
            if (counter == 1 && counter == 2)
            {
                continue;
            }

            string[] fields = record.Split(",");

            int itemID = int.Parse(fields[0]);
            string itemName = fields[1];

            string[] itemGenre = new string[3];
            itemGenre[0] = fields[2];
            itemGenre[1] = fields[3];
            itemGenre[2] = fields[4];

            string itemKey = fields[5];

            UsedToType usedToType = (UsedToType)int.Parse(fields[6]);
            ItemType itemType = (ItemType)int.Parse((string)fields[7]);

            int itemRarity = int.Parse(fields[8]);
            string itemRarityDisplay = fields[9];

            int itemMarginNVC = int.Parse(fields[10]);
            Dictionary<string, int> itemRecipe = new Dictionary<string, int>()
            {
                {fields[12],int.Parse(fields[13])}, {fields[14],int.Parse(fields[15])}, {fields[16],int.Parse(fields[17])}
            };

            string itemSeasonalPopularity = fields[18];
            string itemDescription = fields[19];

            ItemsDataBean item;

            //item = new ItemsDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemRecipe);
            switch (itemType)
            {
                default:
                    item = new ItemsDataBean(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemRecipe);
                    break;
            }


            Debug.Log("item ItemDataBean created : " + item.itemID + " | " + item.itemName);

            allItemList.Add(item);
        }

        inventoryModelContainer.SetAllItem(allItemList);
    }
}