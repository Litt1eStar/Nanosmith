using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;

    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    PlayerItemData itemData;
    GridData inventoryItem;
    GridData otherData;
    ObjectPlacer objectPlacer;

    public RemovingState(
                         Grid grid,
                         PreviewSystem previewSystem,
                         GridData inventoryItem,
                         GridData otherData,
                         ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;


        this.inventoryItem = inventoryItem;
        this.otherData = otherData;
        this.objectPlacer = objectPlacer;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition, Inventory_Controller inventoryController, GameObject childInParent)
    {
        GridData selectedData = null;
        if (inventoryItem != null)
        {
            if (inventoryItem.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
            {
                Debug.Log("InventoryItem Check");
                selectedData = inventoryItem;
            }
            else if (otherData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
            {
                Debug.Log("OtherData Check");
                selectedData = otherData;
            }

            if (selectedData == null)
            {
                Debug.Log("SelectedData is Null");
            }
            else
            {
                gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
                if (gameObjectIndex == -1)
                {
                    return;
                }
                selectedData.RemoveObjectAt(gridPosition);
                Debug.Log("RemoveObjectDone");
                objectPlacer.RemoveObjectAt(gameObjectIndex);
            }
            Vector3 cellPosition = grid.CellToWorld(gridPosition);
            previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
        }
        else
        {
            Debug.Log("InventoryItem is NULL");
        }
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !(inventoryItem.CanPlaceObjectAt(gridPosition, Vector2Int.one) &&
            otherData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
}
