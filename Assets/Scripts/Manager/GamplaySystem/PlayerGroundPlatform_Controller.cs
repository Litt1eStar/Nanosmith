using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundPlatform_Controller : MonoBehaviour
{
    public ObstacleType myType;
    public GameObject attachNode;
    private GameObject startPoint;
    public GameObject playerPrefab;
    public void Init(ObstacleType type)
    {
        myType = type;
   
        if (myType != ObstacleType.Empty)
        {
            string targetName = myType.ToString() + "_ground_object_prefab";
            GameObject targetPrefab = Resources.Load<GameObject>("Prefabs/GridObject_prefab/" + targetName.ToLower());
            Debug.Log("Target Prefab :: " + targetPrefab.name);
            if (targetPrefab != null)
            {
                GameObjectUtil.Instance.AddChild(attachNode, targetPrefab);
            }
            else
            {
                Debug.LogError("TargetPrefab is NULL");
            }

        }
    }

    private void SetEmpty()
    {
        GameObjectUtil.Instance.DestroyAllChildren(attachNode);
        myType = ObstacleType.Empty;
    }

}
