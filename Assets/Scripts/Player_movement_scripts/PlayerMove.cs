using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    private float speedChangeFactor = 1f;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;

    [Header("Gravity")]
    public float gravityMultiplier;
    public float fallMultiplier;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        dashing,
        sprinting,
        air
    }

    public bool dashing;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    // Added public maxYSpeed property
    public float maxYSpeed = 0f;

    private PlayerInputManager playerControls;

    private void StateHandler()
    {
        if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        else if (grounded && playerControls.Player.Sprint.IsPressed())
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
            desiredMoveSpeed = (desiredMoveSpeed < sprintSpeed) ? walkSpeed : sprintSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
                StartCoroutine(SmoothlyLerpMoveSpeed());
            else
                moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime * speedChangeFactor;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void Start()
    {
        playerControls = GetComponent<PlayerInputManager>();
        ConnectControls();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // Adjust drag based on whether grounded or not
        rb.linearDamping = grounded ? groundDrag : 0;

        // Apply custom gravity
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        // If dashing, skip normal movement to preserve the dash trajectory.
        if (!dashing)
            MovePlayer();

        ClampVerticalSpeed();
    }

    // Connect jumping to the input callbacks
    private void ConnectControls()
    {
        playerControls.Player.Jump.performed += TryJump;
    }

    private void MyInput()
    {
        var input = playerControls.Player.Move.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        float forceMultiplier = grounded ? 1f : airMultiplier;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * forceMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.sqrMagnitude > moveSpeed * moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void TryJump(InputAction.CallbackContext context)
    {
        if (readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Jump()
    {
        float velocityChange = jumpForce - rb.linearVelocity.y;
        Vector3 jumpVelocityChange = new Vector3(0f, velocityChange, 0f);
        rb.AddForce(jumpVelocityChange, ForceMode.VelocityChange);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ApplyGravity()
    {
        if (!grounded)
        {
            if (rb.linearVelocity.y < 0)
                rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
            else
                rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    private void ClampVerticalSpeed()
    {
        // If maxYSpeed is set (greater than zero), clamp upward speed to that value.
        if (maxYSpeed > 0 && rb.linearVelocity.y > maxYSpeed)
        {
            Vector3 v = rb.linearVelocity;
            v.y = maxYSpeed;
            rb.linearVelocity = v;
        }
    }
}
