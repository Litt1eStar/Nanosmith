using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Grid_ObjectController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemID;
    [SerializeField] private TextMeshProUGUI FkID;

    public int Rows;
    public int Columns;
    private GameObject attachNode;

    private PlayerItemData inventoryItem;
    public MouseInput mouseInput;

    private void Awake()
    {
        mouseInput = MouseInput.Instance;
    }
    public void Init(PlayerItemData itemInInventory)
    {
        Debug.Log("gridAttachNode :: " + mouseInput.gridAttachNode.name);
        if (mouseInput.gridAttachNode != null)
        {
            attachNode = mouseInput.gridAttachNode;
        }
        else
        {
            Debug.LogError("mouseInput is null");
        }
        inventoryItem = itemInInventory;
        string itemName = inventoryItem.machineDataBean.itemKey;
        string prefabName = itemName + "_prefab";
        Debug.Log("Prefab Name :: " + prefabName);
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/GridObject_prefab/machine/" + prefabName);
        Debug.Log("ItemPrefab :: " + itemPrefab.name);
        if (itemPrefab != null)
        {
            Debug.Log("ItemPrefab to Instantiate :: " + itemPrefab.name);
            GameObjectUtil.Instance.AddChild(attachNode, itemPrefab);
        }
        else
        {
            Debug.LogError("Prefab not found: " + itemPrefab);
        }
    }
    public void GridCells(int row, int column)
    {
        Rows = row;
        Columns = column;
    }

    public void LoadPrefab()
    {

    }
}
