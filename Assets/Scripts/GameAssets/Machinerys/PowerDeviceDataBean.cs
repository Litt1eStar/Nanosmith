using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeviceDataBean : MachineryDataBean
{
    public int powerGeneratePerHour; // 12

    public PowerDeviceDataBean(string itemID,
                               string machineName,
                               string machineType,
                               string itemKey,
                               UsedToType usedToType,
                               ItemType itemType,
                               int machinePriceNVC,
                               string machineDescription,
                               int storageSize,
                               Vector2Int objectSize,
                               int powerGeneratePerHour)
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
        this.powerGeneratePerHour = powerGeneratePerHour;
    }
}
