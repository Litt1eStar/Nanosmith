using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBean
{
    public int itemID; // field 0
    public string itemName; // field 1
    public string[] itemGenre; // field 2 3 4
    public string itemKey; // field 5
    public UsedToType usedToType; // field 6
    public ItemType itemType; //field 7 
    public int itemRarity; // 1, 2, 3, 4, 5 => field 8
    public string itemRarityDisplay; // I, II, III, IV, V => field 9
    public int itemMarginNVC; // field 10
    public Dictionary<string, int> itemRecipe; // field{12 13, 14 15, 16 17}
    public string itemSeasonalPopularity; // field 18
    public string itemDescription; // field 19
    
    
    

    public ItemsDataBean(int itemID, string itemName, string itemKey, string[] itemGenre, UsedToType usedToType,ItemType itemType, string itemDescription, string itemRarityDisplay, int itemMarginNVC, string itemSeasonalPopularity, Dictionary<string, int> itemRecipe)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemKey = itemKey;
        this.itemGenre = itemGenre;
        this.usedToType = usedToType;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.itemRarityDisplay = itemRarityDisplay;
        this.itemMarginNVC = itemMarginNVC;
        this.itemSeasonalPopularity = itemSeasonalPopularity;
        this.itemRecipe = itemRecipe;

    }

}
