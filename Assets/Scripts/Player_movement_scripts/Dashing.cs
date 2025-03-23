using System.Collections;
using UnityEngine;

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
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;
    private Vector3 momentum;

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
        if (Input.GetKeyDown(dashKey) && dashCdTimer <= 0)
        {
            Dash();
        }

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        dashCdTimer = dashCd;
        pm.dashing = true;

        Transform forwardT = useCameraForward ? PlayerCam : orientation;
        Vector3 direction = GetDirection(forwardT);

        // Save momentum for carryover
        momentum = rb.linearVelocity;

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        // Optional: Reset velocity before applying dash
        if (resetVel)
            rb.linearVelocity = Vector3.zero;

        // Start the smooth dash coroutine
        StartCoroutine(SmoothDash(forceToApply));

        Invoke(nameof(ResetDash), dashDuration);
    }

    private IEnumerator SmoothDash(Vector3 force)
    {
        float time = 0f;
        Vector3 initialVelocity = momentum;  // Start with the current momentum

        while (time < dashDuration)
        {
            // Smoothly interpolate between the initial velocity and the desired dash force
            rb.linearVelocity = Vector3.Lerp(initialVelocity, force, time / dashDuration);
            time += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = force;  // Ensure final speed is the desired dash force
    }

    private void ResetDash()
    {
        pm.dashing = false;
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
