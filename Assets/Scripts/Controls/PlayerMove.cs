using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {
    //public PlayerData data;

    //public Transform orientation;

    //private Vector3 moveDirection;
    //private Rigidbody rb;

    //public MovementState state;
    //public enum MovementState {
    //    walking,
    //    dashing,
    //    sprinting,
    //    air
    //}

    //public bool dashing;
    //private MovementState lastState;

    //// Added public maxYSpeed property
    //public float maxYSpeed = 0f;

    //private PlayerInputManager playerControls;

    ////should be a state machine
    //private void StateHandler() {
    //    if (dashing) {
    //        state = MovementState.dashing;
    //        desiredMoveSpeed = data.dashSpeed;
    //        speedChangeFactor = data.dashSpeedChangeFactor;
    //    } else if (grounded && playerControls.Player.Sprint.IsPressed()) {
    //        state = MovementState.sprinting;
    //        desiredMoveSpeed = data.sprintSpeed;
    //    } else if (grounded) {
    //        state = MovementState.walking;
    //        desiredMoveSpeed = data.walkSpeed;
    //    } else {
    //        state = MovementState.air;
    //        desiredMoveSpeed = ( desiredMoveSpeed < data.sprintSpeed ) ? data.walkSpeed : data.sprintSpeed;
    //    }

    //    bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
    //    if (lastState == MovementState.dashing)
    //        keepMomentum = true;

    //    if (desiredMoveSpeedHasChanged) {
    //        if (keepMomentum)
    //            StartCoroutine(SmoothlyLerpMoveSpeed());
    //        else
    //            moveSpeed = desiredMoveSpeed;
    //    }

    //    lastDesiredMoveSpeed = desiredMoveSpeed;
    //    lastState = state;
    //}

    //private void Start() {
    //    playerControls = GetComponent<PlayerInputManager>();
    //    ConnectControls();

    //    rb = GetComponent<Rigidbody>();
    //    rb.freezeRotation = true;
    //    readyToJump = true;
    //}

    //private void Update() {
    //    grounded = Physics.Raycast(transform.position, Vector3.down, data.playerHeight * 0.5f + 0.3f, data.groundLayerMask);

    //    SpeedControl();
    //    StateHandler();

    //    // Adjust drag based on whether grounded or not
    //    rb.linearDamping = grounded ? data.groundDrag : 0;

    //    // Apply custom gravity
    //    ApplyGravity();
    //}

    //private void FixedUpdate() {
    //    // If dashing, skip normal movement to preserve the dash trajectory.
    //    if (!dashing)
    //        MovePlayer();

    //    ClampVerticalSpeed();
    //}

    //// Connect jumping to the input callbacks
    //private void ConnectControls() {
    //    playerControls.Player.Jump.performed += TryJump;
    //}

    //private Vector2 GetLocomotionInput() {
    //    return playerControls.Motion.ReadValue<Vector2>();
    //}



    //private void TryJump(InputAction.CallbackContext context) {
    //    if (readyToJump && grounded) {
    //        readyToJump = false;
    //        Jump();
    //        Invoke(nameof(ResetJump), data.jumpCooldown);
    //    }
    //}

    //private void Jump() {
    //    float velocityChange = data.jumpForce - rb.linearVelocity.y;
    //    Vector3 jumpVelocityChange = new Vector3(0f, velocityChange, 0f);
    //    rb.AddForce(jumpVelocityChange, ForceMode.VelocityChange);
    //}

    //private void ResetJump() {
    //    readyToJump = true;
    //}

    //private void ApplyGravity() {
    //    if (!grounded) {
    //        if (rb.linearVelocity.y < 0)
    //            rb.AddForce(Vector3.down * data.fallMultiplier, ForceMode.Acceleration);
    //        else
    //            rb.AddForce(Vector3.down * data.gravityMultiplier, ForceMode.Acceleration);
    //    }
    //}

    //private void ClampVerticalSpeed() {
    //    // If maxYSpeed is set (greater than zero), clamp upward speed to that value.
    //    if (maxYSpeed > 0 && rb.linearVelocity.y > maxYSpeed) {
    //        Vector3 v = rb.linearVelocity;
    //        v.y = maxYSpeed;
    //        rb.linearVelocity = v;
    //    }
    //}
}
