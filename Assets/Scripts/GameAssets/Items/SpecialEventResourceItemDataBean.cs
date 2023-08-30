using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEventResourceItemDataBean : ItemsDataBean
{
    public SpecialEventResourceItemDataBean(int itemID,
                                            string itemName,
                                            string itemKey,
                                            string[] itemGenre,
                                            UsedToType usedToType,
                                            ItemType itemType,
                                            string itemDescription,
                                            int itemRarity,
                                            string itemRarityDisplay,
                                            float itemMarginNVC,
                                            string itemSeasonalPopularity,
                                            int itemProductionRatePerMinute,
                                            Dictionary<string, int> itemRecipe) :

                                            base(itemID,
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
                                            itemRecipe
                                            )
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
        this.itemRecipe = itemRecipe;
    }

}
