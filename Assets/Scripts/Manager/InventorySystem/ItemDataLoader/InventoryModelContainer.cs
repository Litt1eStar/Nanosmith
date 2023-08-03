using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryModelContainer : MonoBehaviour
{
    public Dictionary<string, ItemsDataBean> gameResourceItemDict = new Dictionary<string, ItemsDataBean>();
    //public Dictionary<string, ConsumableIteamDataBean> gameConsumableItemDict = new Dictionary<string, ConsumableIteamDataBean>();

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
                        gameResourceItemDict[item.itemKey] = (GameResourceItemDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (GameResourceItemDataBean)item);
                    }
                    break;

                case ItemType.NanoGameResourceItem:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (SpecialEventResourceItemDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (SpecialEventResourceItemDataBean)item);
                    }
                    break;

                case ItemType.EenrgyItem:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (SpecialEventResourceItemDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (SpecialEventResourceItemDataBean)item);
                    }
                    break;

                case ItemType.SyntheticItem:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (SpecialEventResourceItemDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (SpecialEventResourceItemDataBean)item);
                    }
                    break;

                case ItemType.GenerateRequireItem:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (GenerateRequiredItemDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (GenerateRequiredItemDataBean)item);
                    }
                    break;
            }
        });
    }

    public List<ItemsDataBean> GetAllItems()
    {
        return gameResourceItemDict.Values.ToList();
    }
}
