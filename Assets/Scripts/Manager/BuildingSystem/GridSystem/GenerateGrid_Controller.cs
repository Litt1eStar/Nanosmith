using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GenerateGrid_Controller : MonoBehaviour
{
    public int Rows;
    public int Columns;

    private Grid_ObjectController gridObj;
    public PlayerItemData gridItemData;
    public GameObject gridCellPrefab;

    public Inventory_Controller inventory;
    public MouseInput mouseInput;
    //public MouseInput clickEvent;

    private void Start()
    {
        InitializedGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // isClickGrid == false
        {
            if (!mouseInput.isClickedGrid)
            {
                AssignItemToGrid();
            }
        }
    }
    private void InitializedGrid()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                GameObject cellGO = Instantiate(gridCellPrefab, new Vector3(row, 0, -column), Quaternion.identity);
                Grid_ObjectController cell = cellGO.GetComponent<Grid_ObjectController>();
            }
        }
    }

    private void AssignItemToGrid()
    {
        if (inventory.InventoryData() != null)
        {
            gridItemData = inventory.InventoryData();
            gridObj = GetComponent<Grid_ObjectController>();
            if (gridItemData != null)
            {
                gridObj.Init(gridItemData);

            }
            else
            {
                Debug.LogError("gridItemData is null");
            }

        }     
    }
}
