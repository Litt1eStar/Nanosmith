using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject shopMachineryItemPanel;
    [SerializeField] private GameObject shopGenerateItemPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryItemPanel;
    [SerializeField] private GameObject machineProductionPanel;
    [SerializeField] private GameObject machineMenuPanel;

    public bool canOpen;
    private void Awake()
    {
        shopMachineryItemPanel.SetActive(false);
        shopGenerateItemPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        inventoryItemPanel.SetActive(false);
        machineProductionPanel.SetActive(false);
        machineMenuPanel.SetActive(false);
        canOpen = true;
    }

    public void TurnOnMenuPanel()
    {
        machineMenuPanel.SetActive(true);
        canOpen = false;
    }
    public void TurnOffMenuPanel()
    {
        machineMenuPanel.SetActive(false);
        canOpen = false;
    }
    public void TurnOnMachienProductionPanel()
    {
        machineProductionPanel.SetActive(true);
        canOpen = false;
    }
    public void TurnOffMachienProductionPanel()
    {
        machineProductionPanel.SetActive(false);
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
