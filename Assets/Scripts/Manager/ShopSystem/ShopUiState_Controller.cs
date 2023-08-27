using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUiState_Controller : MonoBehaviour
{
    [SerializeField] private GameObject shopMachineryItemPanel;
    [SerializeField] private GameObject shopGenerateItemPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryItemPanel;
    public bool canOpen;
    private void Awake()
    {
        canOpen = true;
    }

    public void TurnOnMachienryPanel()
    {
        shopMachineryItemPanel.SetActive(true);
        canOpen = false;
    }

    public void TurnOffMachienryPanel()
    {
        shopMachineryItemPanel.SetActive(false);
        canOpen= true;
    }

    public void TurnOnGenerateItemPanel()
    {
        shopGenerateItemPanel.SetActive(true);
        canOpen = false;
    }

    public void TurnOffGenerateItemPanel()
    {
        shopGenerateItemPanel.SetActive(false);
        canOpen = true;
    }

    public void TurnOnInventoryPanel()
    {
        inventoryPanel.SetActive(true);
        canOpen = false;
    }

    public void TurnOffInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        canOpen = true;
    }

    public void TurnOnItemInventoryPanel()
    {
        inventoryItemPanel.SetActive(true);
        canOpen = false;
    }

    public void TurnOffItemInventoryPanel()
    {
        inventoryItemPanel.SetActive(false);
        canOpen = true;
    }
}
