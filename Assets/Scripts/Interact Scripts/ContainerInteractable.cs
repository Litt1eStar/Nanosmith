using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteractable : MonoBehaviour, IInteractable
{
    public PlayerInteractUI playerInteractUI;

    public string GetInteractText()
    {
        return "Open Container";
    }

    public void Interact(Transform interactorTransform)
    {
        OpenContainer();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void OpenContainer()
    {
        Debug.Log("[Open Container]");
    }

    public void DisplayUI()
    {
        playerInteractUI.StorageManagerContainer.SetActive(true);
    }

    public void TurnOffUI()
    {
        playerInteractUI.StorageManagerContainer.SetActive(false);
    }

}
