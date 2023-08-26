using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    private List<GameObject> placedGameObject = new();

    public int PlaceObject(GameObject itemPrefab, Vector3 position)
    {
        GameObject newObj = Instantiate(itemPrefab);
        newObj.transform.position = position;
        placedGameObject.Add(newObj);
        return placedGameObject.Count - 1;
    }

    public void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObject.Count <= gameObjectIndex)
        {
            return;
        }
        Destroy(placedGameObject[gameObjectIndex]);
        placedGameObject = null;
    }
}
