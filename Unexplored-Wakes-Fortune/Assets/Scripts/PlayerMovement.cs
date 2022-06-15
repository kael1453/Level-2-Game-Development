using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    // For moving forwards and sideways based on orientation, rather than absolute position.
    [SerializeField] Transform orientation;
    [SerializeField] Rigidbody body; // Used for calculating movements such as when grappling.

    public float walkSpeed = 6f;
    public float sprintSpeed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f; // Radius of the groundCheck sphere.
    public LayerMask ground;

    Vector3 velocity;
    public float terminalVelocity = 15f;
    Vector3 move; // The movement vector that the player moves by in the Move() method.
    bool isGrounded;
    public float slopeLimit = 50.0f;

    [Header("Physics")]
    [SerializeField] float pushPower = 0.5f;
    float pushPowerWithSpeed;

    bool isSprinting;


    void Update()
    {
        // Check if the player is on the ground.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        // If player is grounded and has downwards velocity (gravity) then set velocity to something low.
        if (isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = slopeLimit;
            velocity.y = -2f;
        }

        // Jumping.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100.0f; // To make sure that downwards velocity doesn't increase when we are on slopes we cannot walk on.


            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        // Limit the player to a terminal velocity.
        if (velocity.y < -terminalVelocity)
        {
            velocity.y = -terminalVelocity;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }


        // Movement, and whether the player is sprinting or not.
        CheckIfSprinting();
        if (isSprinting)
        {
            // Things multiplied by sprint speed.
            move = (transform.right * xMovement + transform.forward * zMovement) * sprintSpeed + transform.up * velocity.y;
            pushPowerWithSpeed = pushPower * sprintSpeed;
        }
        else
        {
            // Things multiplied by walk speed.
            move = (transform.right * xMovement + transform.forward * zMovement) * walkSpeed + transform.up * velocity.y;
            pushPowerWithSpeed = pushPower * walkSpeed;
        }

        // Do the actual movement.
        controller.Move(move * Time.deltaTime);
    }


    void CheckIfSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    // Push all rigidbodies that we touch.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // No rigidbody or uses a kinematic body, which doesn't work with this.
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us.
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction.
        // Only push objects sideways.
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the push.
        body.velocity = pushDirection * pushPowerWithSpeed;
    }
}