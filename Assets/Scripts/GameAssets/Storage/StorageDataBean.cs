using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageDataBean
{
    public string storageName; //Name of Storage
    public string storageID;
    public string storageDescription;

    public float maxStorage; //Max Capacity of Storage
    public float currentStorage; //Current Amount of Storage
    public float availableSpace;//Available Space of Storage => _storageCapacity - currentAmount

    public bool isFull;

    public ItemType itemType;
    public UsedToType usedToType;

    public StorageDataBean(string storageName, string storageID, string storageDescription, float maxStorage, float currentStorage, float availableSpace, bool isFull, ItemType itemType, UsedToType usedToType)
    {
        this.storageName = storageName;
        this.storageID = storageID;
        this.storageDescription = storageDescription;
        this.maxStorage = maxStorage;
        this.currentStorage = currentStorage;
        this.availableSpace = availableSpace;
        this.isFull = isFull;
        this.itemType = itemType;
        this.usedToType = usedToType;
    }
}
    

