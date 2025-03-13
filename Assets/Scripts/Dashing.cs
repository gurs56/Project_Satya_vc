using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform PlayerCam;
    public tpc cam;  // Ensure this is assigned in the Inspector
    private Rigidbody rb;
    private PlayerMove pm;  // Changed from PlayerMovementDashing to PlayerMove

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;


    [Header("CameraEffects")]
    public float dashFov;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMove>();  // Get the PlayerMove component instead

        if (rb == null)
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        if (pm == null)
            Debug.LogError("PlayerMove component is missing on " + gameObject.name);
        if (cam == null)
            Debug.LogWarning("PlayerCam reference is not assigned on " + gameObject.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
            Dash();

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        //pm.maxYSpeed = maxDashYSpeed;

        //cam.DoFov(dashFov);

        Transform forwardT;

        if (useCameraForward)
            forwardT = PlayerCam; /// where you're looking
        else
            forwardT = orientation; /// where you're facing (no up or down)

        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        //Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }
    private Vector3 delayedForceToApply;

    private void DalayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }
    private void ResetDash()
    {
        pm.dashing = false;
        //pm.maxYSpeed = 0;

        //cam.DoFov(85f);

        if (disableGravity)
            rb.useGravity = true;
    }
    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}
