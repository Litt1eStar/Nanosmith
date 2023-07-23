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

    private float lastMachineStorage;
   
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
        if (robotController.RobotDetectedObject()) //Robot Detected Machine 
        {
            if (!robotBaseClass.hasTakenData) //Robot isn't taken data from Machine
            {
                if (machineResourceGenerating.MachineSendDataToController() >= machineBaseClass._maxOutputStorage)
                {
                    //Debug.Log("MachineSendData Value ::  " + machineResourceGenerating.MachineSendDataToController());
                    lastMachineStorage = machineResourceGenerating.MachineSendDataToController();
                    machineResourceGenerating.ResetProduction();

                    robotBaseClass.currentStorage = lastMachineStorage;
                    robotBaseClass.hasTakenData = true;

                    machineBaseClass._inputResource = null;
                    machineBaseClass.isTakenData = true;
                }          
            }
            else if (robotBaseClass.hasTakenData)//Robot already taken data from Machine
            {
                machineBaseClass.isTakenData = false;
                if (machineBaseClass._inputResource == null)//Machine Input Resource Still empty
                {

                }
            }
        }
        else if (!robotController.RobotDetectedObject())
        {
            //Debug.Log("Have nothing in my sight");
        }
    }
}
