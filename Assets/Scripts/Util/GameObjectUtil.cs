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
}
