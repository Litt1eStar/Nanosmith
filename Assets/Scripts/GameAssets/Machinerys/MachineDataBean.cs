using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDataBean : MachineryDataBean
{
    public int machineProductionMultiplier; // 9
    public int machinePowerConsumptionPerHour; // 10
    public float machineDurability; // 11

    public MachineDataBean(string itemID, string machineName, string machineType, string itemKey, UsedToType usedToType, ItemType itemType,int machinePriceNVC, string machineDescription,int storageSize, int machineProductionMultiplier, int machinePowerConsumptionPerHour, float machineDurability) 
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
        this.machineProductionMultiplier = machineProductionMultiplier;
        this.machinePowerConsumptionPerHour = machinePowerConsumptionPerHour;
        this.machineDurability = machineDurability;
    }
}
