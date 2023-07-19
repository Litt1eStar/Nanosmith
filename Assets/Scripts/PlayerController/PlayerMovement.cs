using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInteractUI playerInteractUI;
    public CharacterController playerCharacterController;
    public float moveSpeed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 0.25f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;

    Vector3 velocity;
    bool isGrounded;
    void Update()
    {
       

        if (playerInteractUI.FreezePlayerMovementByUIDisplaying())
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * x + transform.forward * z;
            movement = Vector3.zero;
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * x + transform.forward * z;
            isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundLayerMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            playerCharacterController.Move(movement * moveSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * (gravity));
            }

            velocity.y += gravity * Time.deltaTime;

            playerCharacterController.Move(velocity * Time.deltaTime);
        }
        

    }

}
