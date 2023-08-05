using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<MachineryDataBean, PlayerItemData> machineryResourceDict = new Dictionary<MachineryDataBean, PlayerItemData>();
    //public Dictionary<string, MachineryDataBean> machineryResourceDict = new Dictionary<string, MachineryDataBean>();

    //public Dictionary<string, ConsumableIteamDataBean> gameConsumableItemDict = new Dictionary<string, ConsumableIteamDataBean>();

    public void AddPlayerInventory(List<PlayerItemData> playerAddItemToList)
    {
        playerAddItemToList.ForEach(item =>
        {
            if (machineryResourceDict.ContainsKey(item.machineDataBean))
            {
                machineryResourceDict[item.machineDataBean].stack+=1;
            }
            else
            {
                machineryResourceDict.Add(item.machineDataBean, item);
            }
        });

        /*machineryResourceDict.Values.ToList().ForEach(playerItems =>
        {
            switch (playerItems.itemType)
            {
                case ItemType.Machine:
                    if (machineryResourceDict.ContainsKey(playerItems))
                    {
                        machineryResourceDict[playerItems] = (MachineryDataBean)playerItems.itemDataBean;
                    }
                    else
                    {
                        machineryResourceDict.Add(playerItems, (MachineryDataBean)playerItems.itemDataBean);
                    }
                    break;
            }
            
        });*/


        Debug.Log("AddPlayerInventory Complete :: " + machineryResourceDict.Count);
        Debug.Log("AddPlayerInventory allMachineryDict :: " + machineryResourceDict.Count);
    }
    
}
    
