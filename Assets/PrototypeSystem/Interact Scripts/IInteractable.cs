using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{   
    void Interact(Transform interactorTransform); //Method For Object Interact

    void DisplayUI();// Method for Display UI

    void TurnOffUI();//Method for Turn Off UI
    string GetInteractText();//Method for change Interact Text
}
