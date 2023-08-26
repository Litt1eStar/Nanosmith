using UnityEngine;

public interface IBuildingState
{
    void EndState();
    void OnAction(Vector3Int gridPosition, Inventory_Controller inventoryController, GameObject childInParent);
    void UpdateState(Vector3Int gridPosition);
}