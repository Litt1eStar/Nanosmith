using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeviceDataBean : MachineryDataBean
{
    public int powerGeneratePerHour; // 12

    public PowerDeviceDataBean(string itemID, string machineName, string machineType, string itemKey, UsedToType usedToType, ItemType itemType, int machinePriceNVC, string machineDescription, int storageSize,int powerGeneratePerHour)
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
        this.itemID = itemID;
        this.machineName = machineName;
        this.machineType = machineType;
        this.itemKey = itemKey;
        this.usedToType = usedToType;
        this.itemType = itemType;
        this.machinePriceNVC = machinePriceNVC;
        this.machineDescription = machineDescription;
        this.storageSize = storageSize;
        this.powerGeneratePerHour = powerGeneratePerHour;
    }
}
