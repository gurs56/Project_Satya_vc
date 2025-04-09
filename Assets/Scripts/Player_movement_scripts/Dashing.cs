using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMove pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("CameraEffects")]
    public tpc cam;
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

    private Vector3 delayedForceToApply;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMove>();
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
        pm.maxYSpeed = maxDashYSpeed; // Allow a higher vertical speed cap during dash

        // Adjust camera FOV for dash effect.
        cam.DoFov(dashFov);

        Transform forwardT;
        if (useCameraForward)
            forwardT = playerCam;
        else
            forwardT = orientation;

        Vector3 direction = GetDirection(forwardT);
        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        // Fix: Reset dash state after a very short delay so the PlayerMove script resumes control right away.
        Invoke(nameof(ResetDash), 0.05f);
    }

    private void DelayedDashForce()
    {
        if (resetVel)
            rb.linearVelocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;
        pm.maxYSpeed = 0; // Reset vertical speed cap

        // Reset camera FOV to a default value (85f in this example)
        cam.DoFov(85f);

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

        if (horizontalInput == 0 && verticalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}
