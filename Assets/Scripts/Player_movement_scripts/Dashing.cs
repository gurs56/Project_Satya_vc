using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputActions))]
public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform PlayerCam;
    public tpc cam;  // Ensure this is assigned in the Inspector
    private Rigidbody rb;
    private PlayerMove pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    private PlayerInputManager playerControls;

    private void Start()
    {
        playerControls = GetComponent<PlayerInputManager>();
        playerControls.Player.Dash.performed += TryDash;

        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMove>();

        if (rb == null)
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        if (pm == null)
            Debug.LogError("PlayerMove component is missing on " + gameObject.name);
        if (cam == null)
            Debug.LogWarning("PlayerCam reference is not assigned on " + gameObject.name);
    }

    private void Update()
    {
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void TryDash(InputAction.CallbackContext context)
    {
        if (dashCdTimer <= 0)
        {
            Dash();
        }
    }

    private void Dash()
    {
        dashCdTimer = dashCd;
        pm.dashing = true;

        // Choose which transform to use for dash direction
        Transform forwardT = useCameraForward ? PlayerCam : orientation;
        Vector3 direction = GetDirection(forwardT);

        // Calculate the dash velocity based solely on horizontal movement
        Vector3 dashVelocity = direction * dashForce;

        // Optionally reset velocity (ensuring vertical velocity is also cleared)
        if (resetVel)
            rb.linearVelocity = Vector3.zero;

        // Apply the horizontal dash force
        rb.linearVelocity = dashVelocity;

        // Reset dash state after dashDuration seconds
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        pm.dashing = false;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection;
        if (allowAllDirections)
            inputDirection = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            inputDirection = forwardT.forward;

        // Default to forward if there is no input
        if (horizontalInput == 0 && verticalInput == 0)
            inputDirection = forwardT.forward;

        // Project the direction onto the horizontal plane to remove any vertical component
        Vector3 flatDirection = Vector3.ProjectOnPlane(inputDirection, Vector3.up);
        return flatDirection.normalized;
    }
}

