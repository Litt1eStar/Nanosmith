using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineryDataBean 
{
    public string itemID; // 0
    public string machineName; // 1
    public string machineType; // 2
    public string itemKey; // 3

    public UsedToType usedToType; // 4
    public ItemType itemType; // 5

    public int machinePriceNVC; // 6
    public string machineDescription; // 7
    public int storageSize; // 8

    public MachineryDataBean(string itemID, string machineName, string machineType, string itemKey, UsedToType usedToType, ItemType itemType, int machinePriceNVC, string machineDescription, int storageSize)
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
    }

    //public int machineProductionMultiplier; // 8
    //public int machineDeviceProperty; // Resource Generator => Power Consumption/hr , power generator => Power generating/hr , battery =>storage size , Storage => storage size
    //public float machineDurability;


    //public float maxOutputStorage; 
    //public float currentStorage;

    //public int machineHeatingValue;
    //public float maxMachineDurability;
    //public float currentMachineDurability;   

    //public ItemsDataBean inputResource;
    //public ItemsDataBean outputResource;

}
