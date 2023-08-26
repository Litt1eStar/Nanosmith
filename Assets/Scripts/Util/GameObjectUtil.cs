using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class GameObjectUtil : MonoSingleton<GameObjectUtil>
{
    public GameObject AddChild(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        if (go != null && parent != null)
        {
            Transform t = go.transform; // Set gameObject Transform
            t.SetParent(parent.transform); // Set gameObjectTransform as child of ParentTransform
            t.localPosition = prefab.transform.localPosition;
            t.localRotation = prefab.transform.localRotation;
            t.localScale = prefab.transform.localScale;
            go.layer = parent.layer;
        }
        return go;
    }


    public List<GameObject> GetChildren(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        Transform[] ts = parent.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.gameObject.name != parent.name)
            {
                list.Add(t.gameObject);
            }
        }
        return list;
    }

    public List<GameObject> GetFirstDeptChildren(GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();
        Transform[] ts = parent.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            bool isParent = t.gameObject == parent;
            bool isSameParent = t.transform.parent.gameObject == parent.gameObject;
            if (!isParent && isSameParent)
            {
                list.Add(t.gameObject);
            }
        }
        return list;
    }
    public void DestroyAllChildren(GameObject parent)
    {
        if (parent.IsDestroyed() || parent.transform == null || parent.transform.childCount == 0)
        {
            return;
        }

        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void DestroyParent(GameObject parent)
    {
        Destroy(parent);
    }

}
