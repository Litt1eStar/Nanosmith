using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineResourceGenerating : MonoBehaviour
{
    public RobotBaseClass robotBaseClass;//Robot SO
    public MachineBaseClass machineBaseClass;//Machine SO
    public StorageBaseClass storageBaseClass;//Storage SO
    public GameResourceBaseClass gameResourceBaseClass;//GameResource SO

    public float robotStorage;//Used to gather data from machine then send it to robot
    private float elapsedTime;

    private void Awake()
    {
        SetOriginalProductionValue();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime / 60; // Convert Time to minute
        if (machineBaseClass._inputResource != null && robotBaseClass.hasTakenData == false && machineBaseClass.currentStorage < machineBaseClass._maxOutputStorage)
        {
            GenerateResource();
        }
        else if (machineBaseClass._inputResource == null && robotBaseClass.hasTakenData == true)
        {
            print("[Input Resource is Empty] MachineStorage :: " + Mathf.FloorToInt(machineBaseClass.currentStorage));
            print("Player Need to put Resource at Input Resource");
            return;
        }


        if (machineBaseClass.isTakenData)
        {
            //Machine already passed data to another object
        }
        else if (!machineBaseClass.isTakenData)
        {
            //Data is still keep in Machine
        }
    }

    public void GenerateResource()
    {
        float actualProdutionRate = machineBaseClass.productionMultiplier * gameResourceBaseClass.itemProductionRatePerMinute; // = 1 * 50 = 50.
        float resourceToGenerate = actualProdutionRate * elapsedTime; //resource to generate per minute
        machineBaseClass.currentStorage = resourceToGenerate;


        //machineBaseClass._maxOutputStorage = machineBaseClass.currentStorage;
        print(Mathf.FloorToInt(machineBaseClass.currentStorage));
        print("[ " + machineBaseClass._maxOutputStorage + " ]");
    }

    public float MachineSendDataToController()
    {
        if (machineBaseClass.currentStorage >= machineBaseClass._maxOutputStorage) // Machine Storage is Full => Send data to Controller
        {
            return machineBaseClass.currentStorage;
        }
        else
        {
            return machineBaseClass.currentStorage;
        }
    }
    public void ResetProduction()//Use after Machine is Full
    {
        machineBaseClass.currentStorage = 0;
        machineBaseClass.productionMultiplier = machineBaseClass.originalProductionMultiplier;
    }

    public void SetOriginalProductionValue()//Use at Awake()
    {
        machineBaseClass.originalProductionMultiplier = machineBaseClass.productionMultiplier;
        machineBaseClass.originalCurrentStorage = 0;
    }

}