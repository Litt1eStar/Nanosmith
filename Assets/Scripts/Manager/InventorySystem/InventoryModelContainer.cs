using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModelContainer : MonoBehaviour
{
    public Dictionary<string, ItemsDataBean> gameResourceItemDict = new Dictionary<string, ItemsDataBean>();
    public Dictionary<string, ConsumableIteamDataBean> gameConsumableItemDict = new Dictionary<string, ConsumableIteamDataBean>();

    public void SetAllItem(List<ItemsDataBean> gameResourceItem)
    {
        gameResourceItem.ForEach(item => {
            if (gameResourceItemDict.ContainsKey(item.itemKey))
            {
                gameResourceItemDict[item.itemKey] = item;
            }
            else
            {
                gameResourceItemDict.Add(item.itemKey, item);
            }

            switch (item.itemType)
            {
                case ItemType.GameResourceItem:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (ItemsDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (ItemsDataBean)item);
                    }
                    break;
                /*case ItemType.ConsumableItem:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (ConsumableIteamDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (ConsumableIteamDataBean)item);
                    }
                    break;*/
            }
        });
    }

}
