using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator, cellIndicator;
    [SerializeField] InputManager inputManager;
    [SerializeField] Grid grid;
    [SerializeField] private PlayerItemData itemData;
    [SerializeField] private Vector2 objectSize;
    [SerializeField] private GameObject parentInventoryContent;
    private int selectedObjectIndex = -1;

    [SerializeField] GameObject gridVisualization;
    [SerializeField] Inventory_Controller inventoryController;


    public Dictionary<Vector3Int, GameObject> sharedData = new();

    private GridData inventoryItemData, generateItem;
    private Renderer previewRenderer;
    private List<GameObject> placedGameObject = new();
    private void Start()
    {
        StopPlacement();
        inventoryItemData = new GridData();
        generateItem = new GridData();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(PlayerItemData selectedItemData)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
        itemData = selectedItemData;
    }

    private void StopPlacement()
    {
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void PlaceStructure()
    {
        /*if (inputManager.IsPointerOverUI())
        {
            return;
        }*/
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        //Debug.Log("GridPosition / MousePosition :: " + gridPosition + " | " + mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition);
        if (placementValidity == false)
        {
            Debug.LogError("Can't Place on that grid :: " + gridPosition);
            return;
        }

        string itemName = itemData.machineDataBean.itemKey;
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/GridObject_prefab/machine/" + itemName + "_prefab");
        Debug.Log("ItemName :: " + itemData.machineDataBean.machineName + " | stack :: " + itemData.stack);
        if (itemData.stack > 0)
        {
            GameObject newObj = Instantiate(itemPrefab);
            newObj.transform.position = grid.CellToWorld(gridPosition) + new Vector3(0, 0, 1);
            placedGameObject.Add(newObj);
            itemData.stack--;
            Debug.Log("Current Stack :: " + itemData.stack);
            if (itemData.stack <= 0)
            {
                int index = inventoryController.inventoryItemList.IndexOf(itemData);
                inventoryController.inventoryItemList.RemoveAt(index);
                Debug.Log("Complete[Remove Item from List]");
                RemoveItemInInvnetory();
            }
        }
        //Used to place object on grid based on objectSize
        GridData selectedData = itemData.machineDataBean.usedToType == UsedToType.UsedToFactory ? inventoryItemData : generateItem;
        selectedData.AddObject(gridPosition, objectSize, int.Parse(itemData.machineDataBean.itemID), placedGameObject.Count - 1);
       
    }

    public void RemoveItemInInvnetory()
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
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition)
    {
        if (itemData != null)
        {
            GridData selectedData = itemData.machineDataBean.usedToType == UsedToType.UsedToFactory ? inventoryItemData : generateItem;
            return selectedData.CanPlaceObjectAt(gridPosition, new Vector2(1, 1));
        }
        else { return false; }
        
    }

  
    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition);
        previewRenderer.material.color = placementValidity ? Color.white : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
