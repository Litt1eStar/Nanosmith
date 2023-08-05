using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public PlayerInteractUI playerInteractUI;
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    public Vector3 mouseMovement;

    private bool isMouseLocked;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isMouseLocked = !isMouseLocked;
            if (isMouseLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
        }
        if (playerInteractUI.FreezePlayerMovementByUIDisplaying())
        {
            xRotation -= 0;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            mouseMovement = Vector3.zero;
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(mouseMovement);
        }
        else
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            mouseMovement = Vector3.up * mouseX;
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(mouseMovement);
        }
        
    }
}
