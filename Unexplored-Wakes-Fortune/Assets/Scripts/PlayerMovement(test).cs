

/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // For moving forwards and sideways based on orientation, rather than absolute position.
    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float walkSpeed = 6f;
    float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.5f;
    [SerializeField] float maxGroundAngle = 46f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    Rigidbody body;

    bool isMoving;


    [Header("Jumping")]
    public float jumpForce = 20f;
    bool desiredJump;

    [Header("Drag")]
    [SerializeField] float extraFall = 10f; // Extra fall speed because the player does not fall fast enough.
    float groundDrag = 6f;
    float airDrag = 0.5f;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckDistance = 0.3f;
    bool isGrounded;
    int stepsSinceLastGrounded; // Mostly used for delayed jumping from a ledge.

    Vector3 slopeMoveDirection;
    RaycastHit slopeHit;
    private bool SlopeBelow()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode left = KeyCode.A;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode forwards = KeyCode.W;
    [SerializeField] KeyCode backwards = KeyCode.S;



    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        MyInput();
        ControlDrag();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        // Check if we're moving or not.
        if (Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(forwards) || Input.GetKey(backwards))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = (orientation.forward * verticalMovement) + (orientation.right * horizontalMovement);
    }

    private void ControlDrag()
    {
        if (isGrounded)
        {
            body.drag = groundDrag;
        }
        else
        {
            body.drag = airDrag;
        }
    }

    private void MovePlayer()
    {
        if (isGrounded && !SlopeBelow())
        {
            body.AddForce(moveDirection.normalized * walkSpeed, ForceMode.Acceleration);
        }
        else if (isGrounded && SlopeBelow())
        {
            body.AddForce(slopeMoveDirection.normalized * walkSpeed, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            body.AddForce((moveDirection.normalized * walkSpeed) * airMultiplier, ForceMode.Acceleration);
        }

        body.AddForce(transform.up * -extraFall, ForceMode.Acceleration);
    }

    private void Jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
    }
}
 */ 