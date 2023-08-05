using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalControlDeviceDataBean : MachineryDataBean
{
    public EnvironmentalControlDeviceDataBean(string itemID, string machineName, string machineType, string itemKey, UsedToType usedToType, ItemType itemType, int machinePriceNVC, string machineDescription, int storageSize)
                            : base(itemID,
                                   machineName,
                                   machineType,
                                   itemKey,
                                   usedToType,
                                   itemType,
                                   machinePriceNVC,
                                   machineDescription,
                                   storageSize
                                   )
    {

    }
}
