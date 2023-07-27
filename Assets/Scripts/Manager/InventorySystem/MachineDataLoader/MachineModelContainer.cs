using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineModelContainer : MonoBehaviour
{
     public Dictionary<string, MachineryDataBean> gameResourceItemDict = new Dictionary<string, MachineryDataBean>();
     //public Dictionary<string, ConsumableIteamDataBean> gameConsumableItemDict = new Dictionary<string, ConsumableIteamDataBean>();

    public void SetAllItem(List<MachineryDataBean> gameResourceItem)
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
                case ItemType.Machine:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (MachineDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (MachineDataBean)item);
                    }
                    break;
                case ItemType.PowerDevice:
                    if (gameResourceItemDict.ContainsKey(item.itemKey))
                    {
                        gameResourceItemDict[item.itemKey] = (PowerDeviceDataBean)item;
                    }
                    else
                    {
                        gameResourceItemDict.Add(item.itemKey, (PowerDeviceDataBean)item);
                    }
                    break;
            }
        });
    }

}
