using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplayInventory
{
    public Dictionary<int, PlayerItemData> gameItemListDict = new Dictionary<int, PlayerItemData>();
    public PlayerGameplayInventory(Dictionary<int, PlayerItemData> gameItemListDict)
    {
        this.gameItemListDict = gameItemListDict;
    }

    public PlayerItemData CheckAndGetItemFromInventoryDataByItemID(string id)
    {
        foreach (PlayerItemData data in gameItemListDict.Values)
        {
            if (data.machineDataBean.itemID == id)
            {
                return data;
            }
        }
        return null;
    }
}
