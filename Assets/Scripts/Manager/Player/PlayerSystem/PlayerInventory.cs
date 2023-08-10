using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<MachineryDataBean, PlayerItemData> allMachineryResourceDict = new Dictionary<MachineryDataBean, PlayerItemData>();
    public Dictionary<ItemsDataBean, PlayerItemData> allItemResourceDict = new Dictionary<ItemsDataBean, PlayerItemData>();

    #region MachineryResourceDict
    public Dictionary<PlayerItemData, MachineryDataBean> machineResourceDict = new Dictionary<PlayerItemData, MachineryDataBean>();
    public Dictionary<PlayerItemData, StorageDataBean> storageResourceDict = new Dictionary<PlayerItemData, StorageDataBean>();
    public Dictionary<PlayerItemData, PowerDeviceDataBean> powerDeviceResourceDict = new Dictionary<PlayerItemData, PowerDeviceDataBean>();
    public Dictionary<PlayerItemData, LabDataBean> labResourceDict = new Dictionary<PlayerItemData, LabDataBean>();
    public Dictionary<PlayerItemData, EnvironmentalControlDeviceDataBean> envDeviceResourceDict = new Dictionary<PlayerItemData, EnvironmentalControlDeviceDataBean>();
    #endregion

    #region GenerateItemResourceDict
    public Dictionary<PlayerItemData, GenerateRequiredItemDataBean> generateItemResourceDict = new Dictionary<PlayerItemData, GenerateRequiredItemDataBean>();
    #endregion

    public void InfoPlayerInventoryTextConsole()
    {
        Debug.Log("--------------------------------------------------------------------------------------");
        Debug.Log("allMachineryResourceDict :: " + allMachineryResourceDict.Count);
        Debug.Log("allItemResourceDict :: " + allItemResourceDict.Count);

        Debug.Log("-----------------------Player Machinery [Machinery Item]------------------------------");
        allMachineryResourceDict.Values.ToList().ForEach(playerItems => 
        {
            switch (playerItems.itemType)
            {
                case ItemType.Machine:
                    if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                    {
                        Debug.Log("Machine Item Object[Machine] :: " + machineResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    else
                    {
                        Debug.Log("Machine Item Object[Machine] :: " + machineResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    break;

                case ItemType.Storage:
                    if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                    {
                        Debug.Log("Machine Item Object[Storage] :: " + storageResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    else
                    {
                        Debug.Log("Machine Item Object :: " + storageResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    break;

                case ItemType.PowerDevice:
                    if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                    {
                        Debug.Log("Machine Item Object[PowerDevice] :: " + powerDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    else
                    {
                        Debug.Log("Machine Item Object :: " + powerDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    break;

                case ItemType.ResearchAndDevelopDevice:
                    if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                    {
                        Debug.Log("Machine Item Object[ResearchAndDevelopDevice] :: " + labResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    else
                    {
                        Debug.Log("Machine Item Object :: " + labResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    break;

                case ItemType.EnvironmentalControlDevice:
                    if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                    {
                        Debug.Log("Machine Item Object[EnvironmentalControlDevice] :: " + envDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    else
                    {
                        Debug.Log("Machine Item Object :: " + envDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    break;
            }

        });

        Debug.Log("-----------------------Player Machinery [Generate Item]------------------------------");
        allItemResourceDict.Values.ToList().ForEach(playerItems =>
        {
            switch (playerItems.itemType)
            {
                case ItemType.GenerateRequireItem:
                    if (allItemResourceDict.ContainsKey(playerItems.itemsDataBean))
                    {
                        Debug.Log("Generate Item Object[Generate Item] :: " + generateItemResourceDict[playerItems].itemName + " | stack :: " + playerItems.stack); //Isn't work for now
                    }
                    else
                    {
                        Debug.Log("Generate Item Object[Generate Item] :: " + generateItemResourceDict[playerItems].itemName + " | stack :: " + playerItems.stack); //Isn't work for now                        }                        
                    }
                    break;
            }
        });

    }

    public void AddPlayerInventoryMachinery(List<PlayerItemData> playerAddItemToList)
    {
        #region AddItemToPlayerInventory
        playerAddItemToList.ForEach(item =>
        {
            if (item != null)
            {
                if (item.machineDataBean != null)
                {
                    if (allMachineryResourceDict.ContainsKey(item.machineDataBean))
                    {
                        allMachineryResourceDict[item.machineDataBean].stack += 1;
                        //Debug.Log("Item Name :: " + item.machineDataBean.machineName + "Stack is increased to " + allMachineryResourceDict[item.machineDataBean].stack + " | Type of Item :: [" + item + "]");
                    }
                    else
                    {
                        allMachineryResourceDict.Add(item.machineDataBean, item);
                    }
                }
                else
                {
                    Debug.LogError("[ item.machineDataBean is null ]");
                }
            }
            else
            {
                Debug.LogError("[ item is null ]");
            }
        });

        Debug.Log("-----------------------Player Item------------------------------");
        allMachineryResourceDict.Values.ToList().ForEach(playerItems =>
        {
            if (playerItems != null)
            {
                switch (playerItems.itemType)
                {
                    case ItemType.Machine:
                        if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                        {
                            machineResourceDict[playerItems] = (MachineryDataBean)playerItems.machineDataBean;
                            Debug.Log("Machine Item Object[Machine] :: " + machineResourceDict[playerItems].machineName + " | stack :: " +  playerItems.stack); //Isn't work for now
                        }
                        else
                        {
                            machineResourceDict.Add(playerItems, (MachineryDataBean)playerItems.machineDataBean);
                            Debug.Log("Machine Item Object[Machine] :: " + machineResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        break;

                    case ItemType.Storage:
                        if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                        {
                            storageResourceDict[playerItems] = (StorageDataBean)playerItems.machineDataBean;
                            Debug.Log("Machine Item Object[Storage] :: " + storageResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        else
                        {
                            storageResourceDict.Add(playerItems, (StorageDataBean)playerItems.machineDataBean);
                            Debug.Log("Machine Item Object :: " + storageResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        break;

                    case ItemType.PowerDevice:
                        if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                        {
                            powerDeviceResourceDict[playerItems] = (PowerDeviceDataBean)playerItems.machineDataBean;
                            Debug.Log("Machine Item Object[PowerDevice] :: " + powerDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        else
                        {
                            powerDeviceResourceDict.Add(playerItems, (PowerDeviceDataBean)playerItems.machineDataBean);
                            Debug.Log("Machine Item Object :: " + powerDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        break;

                    case ItemType.ResearchAndDevelopDevice:
                        if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                        {
                            labResourceDict[playerItems] = (LabDataBean)playerItems.machineDataBean;
                            Debug.Log("Machine Item Object[ResearchAndDevelopDevice] :: " + labResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        else
                        {
                            labResourceDict.Add(playerItems, (LabDataBean)playerItems.machineDataBean);
                            Debug.Log("Machine Item Object :: " + labResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        break;

                    case ItemType.EnvironmentalControlDevice:
                        if (allMachineryResourceDict.ContainsKey(playerItems.machineDataBean))
                        {
                            envDeviceResourceDict[playerItems] = (EnvironmentalControlDeviceDataBean)playerItems.machineDataBean;
                            Debug.Log("Machine Item Object[EnvironmentalControlDevice] :: " + envDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        else
                        {
                            envDeviceResourceDict.Add(playerItems, (EnvironmentalControlDeviceDataBean)playerItems.machineDataBean);
                            Debug.Log("Machine Item Object :: " + envDeviceResourceDict[playerItems].machineName + " | stack :: " + playerItems.stack); //Isn't work for now
                        }
                        break;
                }
            }
            else if(playerItems == null)
            {
                Debug.Log("AddPlayerInventory HAVE ERRORRRRRRR :: [" + playerItems.machineDataBean.machineName + "]");
            }
        });
        #endregion
        #region DisplayData
        Debug.Log("-----------------------Item Amount------------------------------");
        Debug.Log("[PlayerInventory] AddPlayerInventory allMachineryResoruceDict :: " + allMachineryResourceDict.Count);
        Debug.Log("[PlayerInventory] AddPlayerInventory machineResourceDict :: " + machineResourceDict.Count);
        Debug.Log("[PlayerInventory] AddPlayerInventory storageResourceDict :: " + storageResourceDict.Count);
        Debug.Log("[PlayerInventory] AddPlayerInventory powerDeviceResourceDict :: " + powerDeviceResourceDict.Count);
        Debug.Log("[PlayerInventory] AddPlayerInventory ResearchAndDevelopDeviceDict :: " + labResourceDict.Count);
        Debug.Log("[PlayerInventory] AddPlayerInventory EnvironmentalControlDeviceDict :: " + envDeviceResourceDict.Count);
        Debug.Log("----------------------------------------------------------------");
        #endregion
    }

    public void AddPlayerInventoryGenerateItem(List<PlayerItemData> playerAddItemToList)
    {
        playerAddItemToList.ForEach(item =>
        {
            if (item != null)
            {
                if (item.itemsDataBean != null)
                {
                    if (allItemResourceDict.ContainsKey(item.itemsDataBean))
                    {
                        allItemResourceDict[item.itemsDataBean].stack += 1;
                        //Debug.Log("Item Name :: " + item.machineDataBean.machineName + "Stack is increased to " + allMachineryResourceDict[item.machineDataBean].stack + " | Type of Item :: [" + item + "]");
                    }
                    else
                    {
                        allItemResourceDict.Add(item.itemsDataBean, item);
                    }
                }
                else
                {
                    Debug.LogError("[ item.machineDataBean is null ]");
                }
            }
            else
            {
                Debug.LogError("[ item is null ]");
            }
        });

        Debug.Log("-----------------------Player Item------------------------------");
        allItemResourceDict.Values.ToList().ForEach(playerItems =>
        {
            if (playerItems != null)
            {
                switch (playerItems.itemType)
                {
                    case ItemType.GenerateRequireItem:
                        if (allItemResourceDict.ContainsKey(playerItems.itemsDataBean))
                        {
                            generateItemResourceDict[playerItems] = (GenerateRequiredItemDataBean)playerItems.itemsDataBean;
                            Debug.Log("Machine Item Object[Machine] :: " + generateItemResourceDict[playerItems].itemName + " | stack :: " + playerItems.stack); //Isn't work for now
                            break;
                        }
                        else
                        {
                            generateItemResourceDict.Add(playerItems, (GenerateRequiredItemDataBean)playerItems.itemsDataBean);
                            Debug.Log("Machine Item Object[Machine] :: " + generateItemResourceDict[playerItems].itemName + " | stack :: " + playerItems.stack); //Isn't work for now                        }
                            break;
                        }
                }
            }
            else if (playerItems == null)
            {
                Debug.Log("AddPlayerInventory HAVE ERRORRRRRRR :: [" + playerItems.itemsDataBean.itemName + "]");
            }
         });

        Debug.Log("-----------------------Item Amount------------------------------");
        Debug.Log("[PlayerInventory] AddPlayerInventory allItemResourceDict :: " + allItemResourceDict.Count);
        Debug.Log("[PlayerInventory] AddPlayerInventory generateItemResourceDict :: " + generateItemResourceDict.Count);
        Debug.Log("----------------------------------------------------------------");
    }


}
    
