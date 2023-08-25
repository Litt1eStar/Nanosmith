using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDataBean : MachineryDataBean
{
    public StorageDataBean(string itemID,
                           string machineName,
                           string machineType,
                           string itemKey,
                           UsedToType usedToType,
                           ItemType itemType,
                           int machinePriceNVC,
                           string machineDescription,
                           int storageSize,
                           Vector2Int objectSize)
                            : base(itemID,
                                   machineName,
                                   machineType,
                                   itemKey,
                                   usedToType,
                                   itemType,
                                   machinePriceNVC,
                                   machineDescription,
                                   storageSize,
                                   objectSize
                                   )
    {
        
    }
}
