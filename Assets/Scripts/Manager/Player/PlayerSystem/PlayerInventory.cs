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
            Debug.Log("Machine Name :: " + item.machineDataBean.machineName);
            if (item != null)
            {       
                if (machineryResourceDict.ContainsKey(item.machineDataBean))
                {
                    machineryResourceDict[item.machineDataBean].stack += 1;
                    Debug.Log("Stack is increased to " + machineryResourceDict[item.machineDataBean].stack);
                }
                else
                {
                    machineryResourceDict.Add(item.machineDataBean, item);
                }
            }
            else
            {
                Debug.Log("AddPlayerInventory HAVE ERRORRRRRRR");
                return;
            }
        });


        machineryResourceDict.Values.ToList().ForEach(playerItems =>
        {
            if (playerItems != null)
            {
                switch (playerItems.itemType)
                {
                    case ItemType.Machine:
                        if (machineryResourceDict.ContainsKey(playerItems.machineDataBean))
                        {
                            machineResourceDict[playerItems] = (MachineryDataBean)playerItems.machineDataBean;
                            Debug.Log("Machine Item Object :: " + machineResourceDict[playerItems].machineName); //Isn't work for now
                        }
                        else
                        {
                            machineResourceDict.Add(playerItems, (MachineryDataBean)playerItems.machineDataBean);
                            Debug.Log("Machine Item Object :: " + machineResourceDict[playerItems].machineName); //Isn't work for now
                        }
                        break;
                }
            }
            else
            {
                Debug.Log("AddPlayerInventory HAVE ERRORRRRRRR");
            }
        });


        Debug.Log("AddPlayerInventory Complete :: " + machineryResourceDict.Count);
        Debug.Log("AddPlayerInventory allMachineryDict :: " + machineryResourceDict.Count);
    }
    
}
    
