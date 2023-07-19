using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public RobotBaseClass robotBaseClass;
    public MachineBaseClass machineBaseClass;
    public MachineResourceGenerating machineResourceGenerating;

    public GameObject raycastOrigin;
    public LayerMask layerMask;
    public float rayCastDistance = 2f;
    public bool RobotDetectedObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, rayCastDistance, layerMask))
        {
            //Debug.Log("[ROBOT IS DETECTED SOMETHING]");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdatedRobotData()
    {
        robotBaseClass.currentStorage = machineResourceGenerating.robotStorage;
    }
}
