using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryController : MonoBehaviour
{
    public MachineResourceGenerating machineResourceGenerating;
    public MachineBaseClass machineBaseClass;

    public RobotController robotController;
    public RobotBaseClass robotBaseClass;

    public delegate void OnMachineFull(); //delegate that collect fucntion when machine is full
    OnMachineFull machineFull;

   
    private void Update()
    {
        machineFull += MachineCallingRobot;
        machineFull += AssignDataToRobot;
        machineFull();

        
    }
    private void MachineCallingRobot()
    {
        if(machineResourceGenerating.MachineSendDataToController() >= machineBaseClass._maxOutputStorage) //Machine is full => Call robot to collect data
        {
            //Debug.Log("Machine Calling Robot to Collect Data");
            //robotBaseClass.currentStorage = machineResourceGenerating.MachineSendDataToController();
            //Debug.Log("robot Current Storage :: " + robotBaseClass.currentStorage);
        }
    }

    private void AssignDataToRobot()
    {
        if (robotController.RobotDetectedObject())
        {    
            machineResourceGenerating.robotStorage = machineResourceGenerating.MachineSendDataToController();
            robotController.UpdatedRobotData();
            //machineResourceGenerating.ResetProduction();
            robotBaseClass.hasTakenData = true;
            machineBaseClass._inputResource = null;
            return;
            //Debug.Log("robot Current Storage :: " + robotBaseClass.currentStorage + "[Machine Current Storage :: " + machineBaseClass.currentStorage + "]");
        }
        else if(robotController.RobotDetectedObject() == false)
        {
            robotBaseClass.hasTakenData = false;
            
        }
    }
}
