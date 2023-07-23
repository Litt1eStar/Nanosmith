using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    public StorageBaseClass storageBaseClass;
    public MachineBaseClass machineBaseClass;
    private void Awake()
    {

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
