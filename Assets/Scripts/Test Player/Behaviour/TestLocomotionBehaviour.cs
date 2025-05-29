using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TestLocomotionBehaviour : TestSustainedBehaviour<Vector2> {
    [SerializeField]
    private TestLocomotionData data;

    public TestLocomotionData Data {
        get { return data; }
    }

    private CharacterController cc;

    private void Start() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {

    }

    Vector2 NormalizeInput(Vector2 input) {
        return input.magnitude > 1 ? input.normalized : input;
    }

    public override void Act<T>(T data) => throw new NotImplementedException();

    //public float walkSpeed;


    //private float speedChangeFactor = 1f;

    //private float desiredMoveSpeed;
    //private float lastDesiredMoveSpeed;


    ////---//
    //Rigidbody rb;

    //private void Awake() {
    //    rb = GetComponent<Rigidbody>();
    //}

    //private void Update() {

    //}

    //private void MovePlayer() {
    //    var input = data;
    //    moveDirection = orientation.forward * input.y + orientation.right * input.x;
    //    float forceMultiplier = grounded ? 1f : data.airMultiplier;
    //    rb.AddForce(moveDirection.normalized * moveSpeed * 10f * forceMultiplier, ForceMode.Force);
    //}

    //private IEnumerator SmoothlyLerpMoveSpeed() {
    //    float time = 0;
    //    float difference = Mathf.Abs(desiredMoveSpeed - state.moveSpeed);
    //    float startValue = state.moveSpeed;

    //    while (time < difference) {
    //        state.moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
    //        time += Time.deltaTime * speedChangeFactor;
    //        yield return null;
    //    }

    //    state.moveSpeed = desiredMoveSpeed;
    //    speedChangeFactor = 1f;
    //    state.keepMomentum = false;
    //}

    //private void SpeedControl() {
    //    Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
    //    if (flatVel.sqrMagnitude > state.moveSpeed * state.moveSpeed) {
    //        Vector3 limitedVel = flatVel.normalized * state.moveSpeed;
    //        rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
    //    }
    //}


}
