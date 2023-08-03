using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourceItemDataBean : ItemsDataBean
{
    public string itemRecipeKey01; // 11
    public int itemRecipeValue01; // 12

    public string itemRecipeKey02; // 13
    public int itemRecipeValue02; // 14

    public string itemRecipeKey03; // 15
    public int itemRecipeValue03; // 16

    //public int itemProductionRatePerMinute; // field 18

    public GameResourceItemDataBean(int itemID, string itemName, string itemKey, string[] itemGenre, UsedToType usedToType, ItemType itemType, string itemDescription, int itemRarity, string itemRarityDisplay, float itemMarginNVC, int itemProductionRatePerMinute, string itemSeasonalPopularity,
                                    string itemRecipeKey01, string itemRecipeKey02, string itemRecipeKey03, int itemRecipeValue01, int itemRecipeValue02, int itemRecipeValue03)
                                  : base(itemID,
                                         itemName,
                                         itemKey,
                                         itemGenre,
                                         usedToType,
                                         itemType,
                                         itemDescription,
                                         itemRarity,
                                         itemRarityDisplay,
                                         itemMarginNVC,
                                         itemSeasonalPopularity,
                                         itemProductionRatePerMinute
                                         )
    {
        this.itemRecipeKey01 = itemRecipeKey01;
        this.itemRecipeKey02 = itemRecipeKey02;
        this.itemRecipeKey03 = itemRecipeKey03;
        this.itemRecipeValue01 = itemRecipeValue01;
        this.itemRecipeValue02 = itemRecipeValue02;
        this.itemRecipeValue03 = itemRecipeValue03;
        
    }
}
