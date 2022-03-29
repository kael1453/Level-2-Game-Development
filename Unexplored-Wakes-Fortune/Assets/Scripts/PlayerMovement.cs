using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed = 6f;
    public float sprintSpeed = 8f;
    public float gravity = -9.81f;
    public float jumpForce = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f; // Radius of the groundCheck sphere.
    public LayerMask ground;

    Vector3 velocity;
    public float terminalVelocity = 15f;
    Vector3 move; // The movement vector that the player moves by in the Move() method.
    bool isGrounded;
    public float slopeLimit = 50.0f;

    bool isSprinting;


    void Update()
    {
        // Check if the player is on the ground.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        // If player is grounded and has downwards velocity (gravity) then set velocity to something low.
        if(isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = slopeLimit;
            velocity.y = -2f;
        }
        
        // Jumping.
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100.0f; // To make sure that downwards velocity doesn't increase when we are on slopes we cannot walk on.

            
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            
        }

        // amogus
        if(velocity.y < -terminalVelocity)
            {
                velocity.y = -terminalVelocity;
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }
        

        // Movement, and whether the player is sprinting or not.
        CheckIfSprinting();
        if(isSprinting)
        {
            move = (transform.right * xMovement + transform.forward * zMovement) * sprintSpeed + transform.up * velocity.y;
        }
        else
        {
            move = (transform.right * xMovement + transform.forward * zMovement) * walkSpeed + transform.up * velocity.y;
        }

        
        // Do the actual movement.
        controller.Move(move * Time.deltaTime);
    }

    
    void CheckIfSprinting()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }
}