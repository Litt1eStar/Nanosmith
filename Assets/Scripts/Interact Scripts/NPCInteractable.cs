using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour , IInteractable
{
    public PlayerInteractUI playerInteractUI;
    [SerializeField] private string interactText;


    public GameObject NPC;
    public void Interact(Transform InteractorTransform)
    {
        
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public void DisplayUI()
    {
        playerInteractUI.NpcManagerContainer.SetActive(true);
    }

    public void TurnOffUI()
    {
        playerInteractUI.NpcManagerContainer.SetActive(false);
    }
}
