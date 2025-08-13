using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsHandler)/*, typeof(TestFacingBehaviour)*/)]
public class TestLocomotionBehaviour : ATestBehaviour<Vector2> {
    public TestLocomotionData data;

    private PhysicsHandler physicsHandler;

    private float speed;

    public override void Act(Vector2 input) {
        var normalInput = NormalizeInput(input);

        var orientedInput = normalInput.x * transform.right + normalInput.y * transform.forward;

        //delta time applied to velocity in physicsHandler
        physicsHandler.SetQueuedVelocity(this, orientedInput * speed);
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();

        Action setDefaultSpeed = () => { speed = data.speed; };

        setDefaultSpeed.Invoke();

        physicsHandler.onAirborneBegin.AddListener(() => { speed = data.airSpeed; });

        //use air speed when not on ground
        physicsHandler.GroundDetector.onGrounded.AddListener(() => { setDefaultSpeed.Invoke(); });
    }

    Vector2 NormalizeInput(Vector2 input) {
        return input.magnitude > 1 ? input.normalized : input;
    }
}
