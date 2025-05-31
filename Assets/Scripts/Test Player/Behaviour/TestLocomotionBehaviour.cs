using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsHandler))]
public class TestLocomotionBehaviour : TestSustainedBehaviour<Vector2> {
    public TestLocomotionData data;

    private PhysicsHandler physicsHandler;
    public override void Act(Vector2 input) {
        var normalInput = NormalizeInput(input);

        var orientedInput = normalInput.x * transform.right + normalInput.y * transform.forward;

        //use air speed when not on ground
        var speed = physicsHandler.IsGrounded ? data.speed : data.airSpeed;

        //delta time applied to velocity in physicsHandler
        physicsHandler.SetQueuedVelocity(this, orientedInput * speed);
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();
    }

    Vector2 NormalizeInput(Vector2 input) {
        return input.magnitude > 1 ? input.normalized : input;
    }
}
