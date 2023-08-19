using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData : MonoBehaviour
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObject(Vector3Int gridPosition, Vector2 objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this position {pos}");
            }
            Debug.Log("PositionToOccupy :: (" + pos.x + "," + pos.y + "," + pos.z +")");
            placedObjects[pos] = data;
            Debug.Log("placedObject :: " + placedObjects[pos].ID);
        }
    }

    public List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2 objectSize)
    {
        List<Vector3Int> returnVal = new(); // gridPosition = (3, 0 ,2) : objectSize = (2,2) => returnVal
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


