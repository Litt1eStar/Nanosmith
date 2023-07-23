using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineryDataBean 
{
    public string machineName;
    public string machineDescription;

    public float maxOutputStorage; 
    public float currentStorage;

    public int maxMachinePower;
    public int currentMachinePower;

    public int machineHeatingValue;
    public float maxMachineDurability;
    public float currentMachineDurability;

    public float productionMultiplier;
    public float originalProductionMultiplier;

    public ItemsDataBean inputResource;
    public ItemsDataBean outputResource;

    public UsedToType usedToType;
    public ItemType itemType;

    public MachineryDataBean(string machineName, string machineDescription, float maxOutputStorage, float currentStorage, int maxMachinePower, int currentMachinePower, int machineHeatingValue, float maxMachineDurability, float currentMachineDurability, float productionMultiplier, float originalProductionMultiplier, UsedToType usedToType, ItemType itemType, ItemsDataBean inputResource, ItemsDataBean outputResource)
    {
        this.machineName = machineName;
        this.machineDescription = machineDescription;
        this.maxOutputStorage = maxOutputStorage;
        this.currentStorage = currentStorage;
        this.maxMachinePower = maxMachinePower;
        this.currentMachinePower = currentMachinePower;
        this.machineHeatingValue = machineHeatingValue;
        this.maxMachineDurability = maxMachineDurability;
        this.currentMachineDurability = currentMachineDurability; 
        this.productionMultiplier = productionMultiplier;
        this.originalProductionMultiplier = originalProductionMultiplier;
        this.usedToType = usedToType;
        this.itemType = itemType;
        this.inputResource = inputResource;
        this.outputResource = outputResource;
    }
}
