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
    public float itemMarginNVC; // field 10
    public string itemSeasonalPopularity; // field 17
    public string itemDescription; // field 18
    public int itemProductionRatePerMinute;



    public ItemsDataBean(int itemID, string itemName, string itemKey, string[] itemGenre, UsedToType usedToType,ItemType itemType, string itemDescription, int itemRarity, string itemRarityDisplay, float itemMarginNVC, string itemSeasonalPopularity, int itemProductionRatePerMinute)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemKey = itemKey;
        this.itemGenre = itemGenre;
        this.usedToType = usedToType;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.itemRarity = itemRarity;
        this.itemRarityDisplay = itemRarityDisplay;
        this.itemMarginNVC = itemMarginNVC;
        this.itemSeasonalPopularity = itemSeasonalPopularity;
        this.itemProductionRatePerMinute = itemProductionRatePerMinute;
    }

}
