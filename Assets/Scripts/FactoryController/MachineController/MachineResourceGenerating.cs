using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineResourceGenerating : MonoBehaviour
{
    public RobotBaseClass robotBaseClass;
    public MachineBaseClass machineBaseClass;
    public StorageBaseClass storageBaseClass;
    public GameResourceBaseClass gameResourceBaseClass;

    public float robotStorage;
    private float elapsedTime;

    private bool isGenerate;
    private void Awake()
    {
        machineBaseClass.originalProductionMultiplier = machineBaseClass.productionMultiplier;
        machineBaseClass.originalCurrentStorage = 0;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime / 60; // Convert Time to minute
        //Debug.Log("Value of SendDataToController :: " + SendDataToController());

        if (machineBaseClass._inputResource != null && robotBaseClass.hasTakenData == false && machineBaseClass.currentStorage < machineBaseClass._maxOutputStorage)
        {
            GenerateResource();
        }
        if (machineBaseClass._inputResource == null)
        {
            Debug.Log("ResetProduction");
            ResetProduction();
            machineBaseClass.currentStorage = machineBaseClass.originalCurrentStorage;
            machineBaseClass.productionMultiplier = machineBaseClass.originalProductionMultiplier;
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
    public void ResetProduction()
    {
        machineBaseClass.currentStorage = 0;
        machineBaseClass.productionMultiplier = 0;
    }

}
