using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInteractable : MonoBehaviour, IInteractable
{
    public PlayerInteractUI playerInteractUI;

    public void Interact(Transform interactorTransform)
    {
        
    }
    public string GetInteractText()
    {
        return "Machine Is Ready";
    }
    public void DisplayUI()
    {
        playerInteractUI.MachineManagerContainer.SetActive(true);
    }

    public void TurnOffUI()
    {
        playerInteractUI.MachineManagerContainer.SetActive(false);
    }
}
