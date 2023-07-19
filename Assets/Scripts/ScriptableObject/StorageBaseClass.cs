using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StorageObject" , menuName = "StorageObj/storage")]
public class StorageBaseClass : ScriptableObject
{
    public string _storageName; //Name of Storage

    public float _storageCapacity; //Max Capacity of Storage
    public float currentStorage; //Current Amount of Storage
    public float availableSpace;//Available Space of Storage => _storageCapacity - currentAmount

    public bool isFull;
    public bool isMachineFull;


}
