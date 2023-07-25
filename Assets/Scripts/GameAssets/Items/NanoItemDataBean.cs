using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoItemDataBean : ItemsDataBean
{
    public float productionTimeToGenerateMultiplier;
    public NanoItemDataBean(int itemID, string itemName, string itemKey, string[] itemGenre, UsedToType usedToType, ItemType itemType, string itemDescription, string itemRarityDisplay, float productionMultiplier, Dictionary<string, int> itemRecipe, string itemSeasonalPopularity, int itemMarginNVC)
                                    : base(itemID, itemName, itemKey, itemGenre, usedToType, itemType, itemDescription, itemRarityDisplay, itemMarginNVC, itemSeasonalPopularity, itemRecipe)
    {
        
    }
}
