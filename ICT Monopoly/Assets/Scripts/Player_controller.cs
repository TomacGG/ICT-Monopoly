using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] public float mouseSensitivity = 3.5f;
    [SerializeField] private bool lockCursor = true;

    [SerializeField] public float walkSpeed = 6.0f;

    float cameraPitch = 0.0f;
    private CharacterController controller = null;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

    }

    void UpdateMovement()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}