using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float gravity = -20f;

    [Header("Look")]
    [SerializeField] float mouseSensitivity = 0.1f;
    [SerializeField] float maxLookAngle = 85f;

    CharacterController controller;
    InputAction moveAction, lookAction, jumpAction;
    float verticalVelocity;
    float pitch; // up/down camera angle

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Actions from the project-wide InputSystem_Actions asset
        moveAction = InputSystem.actions.FindAction("Player/Move");
        lookAction = InputSystem.actions.FindAction("Player/Look");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // hide cursor and keep it in the window
        Cursor.visible = false;
    }

        void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        Vector2 look = lookAction.ReadValue<Vector2>() * mouseSensitivity;

        // Yaw: turn the whole body left/right
        transform.Rotate(Vector3.up * look.x);

        // Pitch: tilt only the camera up/down, clamped so you can't flip over
        pitch = Mathf.Clamp(pitch - look.y, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

        void Move()
    {
        // WASD -> direction relative to where the player is facing
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        move = Vector3.ClampMagnitude(move, 1f) * moveSpeed;

        if (controller.isGrounded)
        {
            // Small downward force keeps isGrounded reliable on slopes
            if (verticalVelocity < 0f) verticalVelocity = -2f;

            // v = sqrt(2gh) gives a jump that reaches exactly jumpHeight
            if (jumpAction.WasPressedThisFrame())
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }
}
