using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotObject" , menuName = "RobotObj/Robot")]

public class RobotBaseClass : ScriptableObject
{
    public float robotMaxStorage;
    public float currentStorage;

    public float robotMaxPower;
    public float robotCurrentPower;

    public float robotSpeed;

    public Transform robotBaseStanding;//Use to locate that where robot is standby
    public Transform robotDestinationStanding;//Use to locate that where robot is aheading to

    public bool hasTakenData;

}
