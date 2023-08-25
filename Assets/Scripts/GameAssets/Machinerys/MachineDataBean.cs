using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDataBean : MachineryDataBean
{
    public int machineProductionMultiplier; // 9
    public int machinePowerConsumptionPerHour; // 10
    public float machineDurability; // 11

    public MachineDataBean(string itemID,
                           string machineName,
                           string machineType,
                           string itemKey,
                           UsedToType usedToType,
                           ItemType itemType,
                           int machinePriceNVC,
                           string machineDescription,
                           int storageSize,
                           Vector2Int objectSize,
                           int machineProductionMultiplier,
                           int machinePowerConsumptionPerHour,
                           float machineDurability) 
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
        this.machineProductionMultiplier = machineProductionMultiplier;
        this.machinePowerConsumptionPerHour = machinePowerConsumptionPerHour;
        this.machineDurability = machineDurability;
    }
}
