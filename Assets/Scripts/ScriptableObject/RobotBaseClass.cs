using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotObject" , menuName = "RobotObj/Robot")]

public class RobotBaseClass : ScriptableObject
{
    public float _robotCapacity;
    public float currentStorage;
    public float _robotSpeed;

    public bool hasTakenData;

}
