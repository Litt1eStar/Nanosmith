using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    public StorageBaseClass storageBaseClass;
    public MachineBaseClass machineBaseClass;
    private void Awake()
    {
        Debug.Log("[Storage Name :: " + storageBaseClass._storageName + "]");
        Debug.Log("[Storage Capacity :: " + storageBaseClass._storageCapacity + "]");
        Debug.Log("[Current Amount of Storage :: " + storageBaseClass.currentStorage + "]");
    }

    private void Update()
    {
        //StorageUpdate();
    }

    public void StorageUpdate()
    {
        storageBaseClass.currentStorage = machineBaseClass.currentStorage;
        storageBaseClass.availableSpace = storageBaseClass._storageCapacity - storageBaseClass.currentStorage;

        if (storageBaseClass.isFull == true)
        {
            Debug.Log("Storage is FULL");
        }
        else if (storageBaseClass.isFull == false)
        {
            Debug.Log("Storage is Empty");
        }
    }
}
