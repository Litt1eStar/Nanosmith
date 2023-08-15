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
}
