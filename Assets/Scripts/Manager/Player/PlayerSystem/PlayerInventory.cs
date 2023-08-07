using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<MachineryDataBean, PlayerItemData> machineryResourceDict = new Dictionary<MachineryDataBean, PlayerItemData>();
    public Dictionary<PlayerItemData, MachineryDataBean> machineResourceDict = new Dictionary<PlayerItemData, MachineryDataBean>();
    public void AddPlayerInventory(List<PlayerItemData> playerAddItemToList)
    {
        playerAddItemToList.ForEach(item =>
        {
            if (machineryResourceDict.ContainsKey(item.machineDataBean))
            {
                machineryResourceDict[item.machineDataBean].stack +=1;
            }
            else
            {
                machineryResourceDict.Add(item.machineDataBean, item);
            }
        });

        machineryResourceDict.Values.ToList().ForEach(playerItems =>
        {
            switch (playerItems.itemType)
            {
                case ItemType.Machine:
                    if (machineryResourceDict.ContainsKey(playerItems.machineDataBean))
                    {
                        machineResourceDict[playerItems] = (MachineryDataBean)playerItems.machineDataBean;
                    }
                    else
                    {
                        machineResourceDict.Add(playerItems, (MachineryDataBean)playerItems.machineDataBean);
                    }
                    break;
            }
            
        });


        Debug.Log("AddPlayerInventory Complete :: " + machineryResourceDict.Count);
        Debug.Log("AddPlayerInventory allMachineryDict :: " + machineryResourceDict.Count);
    }
    
}
    
