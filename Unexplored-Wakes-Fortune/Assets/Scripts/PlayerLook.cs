using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] Transform playerBody;

    // Change in mouse position from frame to frame.
    float mouseX;
    float mouseY;

    // Rotation based on the mouse movement.
    float xRotation;
    float yRotation;

    [Header("Weapon Sway Settings")]
    [SerializeField] private float smooth = 8f;
    [SerializeField] private float swayMultiplier = 0.05f;
    [SerializeField] private float maxSway = 0.5f;

    [SerializeField] Transform weaponHolder;
    private Vector3 swayStartingPosition;
    


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        swayStartingPosition = weaponHolder.transform.localPosition;
    }

    private void Update()
    {
        MyInput();

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        UpdateSway();
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        xRotation -= mouseY * mouseSensitivity;        
        yRotation += mouseX * mouseSensitivity;
        
        // Make sure we cannot look upside down.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void UpdateSway()
    {
        // Get sway movement based on actual camera rotation, so sway doesn't happen when unable to look up or down any further.
        float xSwayMovement = mouseX * swayMultiplier;
        float ySwayMovement = mouseY * swayMultiplier;

        xSwayMovement = Mathf.Clamp(xSwayMovement, -maxSway, maxSway);
        ySwayMovement = Mathf.Clamp(ySwayMovement, -maxSway, maxSway);

        // Calculate target position.
        Vector3 lastPosition = new Vector3(-xSwayMovement, -ySwayMovement, 0);

        // Position the weapon.
        weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition, lastPosition + swayStartingPosition, smooth * Time.deltaTime);
    }

}
