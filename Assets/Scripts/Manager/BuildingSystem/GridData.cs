using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    //Add Object to placedObject => {Position of Object, ObjectData} 
    // ((0,0),A), ((1,0),A), ((2,0),A)
    // ((0,1),A), ((1,1),A), ((2,1),A)
    // ((0,2),A), ((1,2),A), ((2,2),A)
    public void AddObject(Vector3Int gridPosition, Vector2 objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        Debug.Log("placedObjectIndex :: " + placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                Debug.Log("This Grid Have Placed");
                throw new Exception($"Dictionary already contains this position {pos}");
            }
            //Debug.Log("PositionToOccupy :: (" + pos.x + "," + pos.y + "," + pos.z +")");
            placedObjects[pos] = data;
            Debug.Log("placedObjectPosition :: " + placedObjects[pos].occupiedPositions + " | placedObjectIndex :: " + placedObjectIndex);
        }
    }

    public List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2 objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2 objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }
        return true;
    }

    public int GetRepresentationIndex(Vector3Int gridPosition)
    {
        Debug.Log(placedObjects.Values.ToString());
        if(placedObjects.ContainsKey(gridPosition) == false)
        {
            return -1;
        }
        return placedObjects[gridPosition].PlacedObjectIndex;
    }

    public void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}
public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }
    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObejctIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObejctIndex;
    }
}


