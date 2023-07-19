using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[CreateAssetMenu(fileName = "Machine" , menuName = "Machinery/Machine")]
public class MachineBaseClass : ScriptableObject
{
    public string _machineName;
    public string _machineDescription;
    public string _machineType;

    public float _maxOutputStorage; //condition factor

    public float currentStorage;
    public float originalCurrentStorage;

    public int machineHeatingValue;
    public int currentMachinePower; //condition factor

    public float machineDurability; //condition factor
    public float productionMultiplier;
    public float originalProductionMultiplier;


    public GameResourceBaseClass _inputResource; // Input Resource have Tag named "GeneralResource"
    public GameObject _outputResource;
   
    
}
