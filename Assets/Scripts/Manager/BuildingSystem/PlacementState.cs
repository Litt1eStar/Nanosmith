using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState :  IBuildingState
{
    private int selectedObjectIndex = -1;

    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    PlayerItemData itemData;
    GridData inventoryItem;
    GridData otherData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          PlayerItemData itemData,
                          GridData inventoryItem,
                          GridData otherData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.itemData = itemData;
        this.inventoryItem = inventoryItem;
        this.otherData = otherData;
        this.objectPlacer = objectPlacer;

        string itemName = itemData.machineDataBean.itemKey;
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/GridObject_prefab/machine/" + itemName + "_prefab");
        previewSystem.StartShowingPlacementPreview(itemPrefab,
                                                   itemData.machineDataBean.objectSize);
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();

    }

    public void OnAction(Vector3Int gridPosition, Inventory_Controller inventoryController, GameObject childInParent)
    {
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
            int index = objectPlacer.PlaceObject(itemPrefab, grid.CellToWorld(gridPosition));
            itemData.stack--;
            Debug.Log("Index " + index);

            if (itemData.stack <= 0)
            {
                int itemIndex = inventoryController.inventoryItemList.IndexOf(itemData);
                inventoryController.inventoryItemList.RemoveAt(itemIndex);
                Debug.Log("Complete[Remove Item from List]");
                RemoveItemInInvnetory(childInParent);
            }

            GridData selectedData = itemData.machineDataBean.usedToType == UsedToType.UsedToFactory ? inventoryItem : otherData;
            selectedData.AddObject(gridPosition,
                                   itemData.machineDataBean.objectSize,
                                   int.Parse(itemData.machineDataBean.itemID),
                                   index);
        }
        //Used to place object on grid based on objectSize


        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition)
    {
        if (itemData != null)
        {
            GridData selectedData = itemData.machineDataBean.usedToType == UsedToType.UsedToFactory ? inventoryItem : otherData;
            return selectedData.CanPlaceObjectAt(gridPosition, itemData.machineDataBean.objectSize);
        }
        else { return false; }

    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }

    public void RemoveItemInInvnetory(GameObject childParent)
    {
        List<GameObject> childInParent = GameObjectUtil.Instance.GetChildren(childParent);
        string itemToDestroyName = itemData.machineDataBean.machineName;
        childInParent.ForEach(i =>
        {
            if (i.name == itemData.machineDataBean.itemKey + "_prefab(InventoryPrefab)")
            {
                Debug.Log(i.name);
                GameObjectUtil.Instance.DestroyParent(i);
                Debug.Log("Destroy Complete");
            }
            else
            {
                Debug.Log("Have no Object To Destroy");
                return;
            }
        });
    }

}
