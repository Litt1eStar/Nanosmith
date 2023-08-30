using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResourceItemDataBean : ItemsDataBean
{
    public GameResourceItemDataBean(int itemID,
                                    string itemName,
                                    string itemKey,
                                    string[] itemGenre,
                                    UsedToType usedToType,
                                    ItemType itemType,
                                    string itemDescription,
                                    int itemRarity,
                                    string itemRarityDisplay,
                                    float itemMarginNVC,
                                    int itemProductionRatePerMinute,
                                    string itemSeasonalPopularity,
                                    Dictionary<string, int> itemRecipe)
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
                                         itemProductionRatePerMinute,
                                         itemRecipe)

    {
        //
    }

 
}

