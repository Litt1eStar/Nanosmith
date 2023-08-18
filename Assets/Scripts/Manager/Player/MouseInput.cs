using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
    PlayerGameplayData currentData;
    List<PlayerItemData> inventoryItemList = new List<PlayerItemData>();
    public bool isClickedGrid;

    public LayerMask gridLayermask;
    public GameObject gridAttachNode;

    private Transform targetData;
    private List<GameObject> targetChilderList;

    public static MouseInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        targetChilderList = new List<GameObject>();
        isClickedGrid = false;
    }

    public Transform GetTargetData()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10f, gridLayermask))
        {
            Transform objectHit = hit.transform;
            //objectHit.gameObject.SetActive(false);
            return objectHit;
        }
        else
        {
            return null;
        }
    }

    public GameObject GetChildenData()
    {
        if (GetTargetData() != null)
        {
            targetData = GetTargetData();
            targetChilderList = GameObjectUtil.Instance.GetChildren(targetData.gameObject);

            if (targetChilderList.Count > 1) // Make sure there's a child at index 1
            {
                targetChilderList[1].GetComponent<Renderer>().material.color = Color.red;

                // Assign the child to gridAttachNode
                gridAttachNode = targetChilderList[1]; // Assuming gridAttachNode is a GameObject variable
                Debug.Log(gridAttachNode.name);
                return targetChilderList[1];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

}
