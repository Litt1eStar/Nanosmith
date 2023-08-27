using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator;
    [SerializeField] InputManager inputManager;
    [SerializeField] Grid grid;
    [SerializeField] private PlayerItemData itemData;
    [SerializeField] private Vector2Int objectSize;
    [SerializeField] private GameObject parentInventoryContent;
    private int selectedObjectIndex = -1;

    [SerializeField] GameObject gridVisualization;
    [SerializeField] Inventory_Controller inventoryController;
    [SerializeField] private ObjectPlacer objectPlacer;

    [SerializeField] private PreviewSystem previewSystem;
    public Dictionary<Vector3Int, GameObject> sharedData = new();

    private GridData inventoryItemData, generateItem;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    IBuildingState buildingState;
    private void Start()
    {
        StopPlacement();
        inventoryItemData = new GridData();
        generateItem = new GridData();
    }

    public void StartPlacement(PlayerItemData selectedItemData)
    {
        itemData = selectedItemData;
        string itemName = itemData.machineDataBean.itemKey;
        Debug.Log("ItemKey :: " + itemName);
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/GridObject_prefab/machine/" + itemName + "_prefab");
        Debug.Log("itemPrefab Path :: " + itemPrefab);

        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(int.Parse(itemData.machineDataBean.itemID),
                                           grid,
                                           previewSystem,
                                           itemData,
                                           inventoryItemData,
                                           generateItem,
                                           objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        
        if (inventoryItemData != null)
        {
            buildingState = new RemovingState(grid, previewSystem, inventoryItemData, generateItem, objectPlacer);
            inputManager.OnClicked += PlaceStructure;
            inputManager.OnExit += StopPlacement;
        }
        else
        {
            Debug.LogError("InventoryItem is NULL");
        }

    }
    private void StopPlacement()
    {
        if (buildingState == null)
        {
            return;
        }
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUIElement())
        {
            Debug.Log("Your Cursor is Over UI");
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        //Debug.Log("GridPosition / MousePosition :: " + gridPosition + " | " + mousePosition);

        buildingState.OnAction(gridPosition, inventoryController, parentInventoryContent);

    }

  /* public void RemoveItemInInvnetory()
    {
        List<GameObject> childInParent = GameObjectUtil.Instance.GetChildren(parentInventoryContent);
        string itemToDestroyName = itemData.machineDataBean.machineName;
        childInParent.ForEach(i =>
        {
            if (i.name == itemData.machineDataBean.itemKey + "_prefab(InventoryPrefab)")
            {
                Debug.Log(i.name);
                Destroy(i);
                Debug.Log("Destroy Complete");
            }
            else
            {
                Debug.Log("Have no Object To Destroy");
                return;
            }
        });
    }*/

    //private bool CheckPlacementValidity(Vector3Int gridPosition)
    //{
    //    if (itemData != null)
    //    {
    //        GridData selectedData = itemData.machineDataBean.usedToType == UsedToType.UsedToFactory ? inventoryItemData : generateItem;
    //        return selectedData.CanPlaceObjectAt(gridPosition, itemData.machineDataBean.objectSize);
    //    }
    //    else { return false; }
        
    //}

  
    private void Update()
    {
        if (buildingState == null)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }
}
