using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDataBean
{
    public string robotName;
    public string robotID;
    public string robotDescription;

    public float robotMaxStorage;
    public float currentStorage;

    public float robotMaxPower;
    public float robotCurrentPower;

    public float robotMaxSpeed;
    public float robotCurrentSpeed;

    public bool hasTakenData;

    public Transform robotBaseStanding;//Use to locate that where robot is standby
    public Transform robotDestinationStanding;//Use to locate that where robot is aheading to

    public UsedToType usedToType;
    public ItemType itemType;

    public RobotDataBean(string robotName, string robotID, string robotDescription, float robotMaxStorage, float currentStorage, float robotMaxPower, float robotCurrentPower, float robotMaxSpeed, float robotCurrentSpeed, bool hasTakenData, Transform robotBaseStanding, Transform robotDestinationStanding, UsedToType usedToType, ItemType itemType) 
    { 
        this.robotName = robotName;
        this.robotID = robotID;
        this.robotDescription = robotDescription;
        this.robotMaxStorage = robotMaxStorage;
        this.currentStorage = currentStorage;
        this.robotMaxPower = robotMaxPower;
        this.robotCurrentPower = robotCurrentPower;
        this.robotMaxSpeed = robotMaxSpeed;
        this.robotCurrentSpeed = robotCurrentSpeed;
        this.hasTakenData = hasTakenData;
        this.robotBaseStanding = robotBaseStanding;
        this.robotDestinationStanding = robotDestinationStanding;
        this.usedToType = usedToType;
        this.itemType = itemType;
    }
}


