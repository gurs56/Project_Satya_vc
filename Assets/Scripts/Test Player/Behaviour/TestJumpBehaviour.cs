using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(TestLocomotionData), typeof(PhysicsHandler))]
public class TestJumpBehaviour : TestInstataneousBehaviour {
    [SerializeField]
    private TestJumpData data;

    private bool readyToJump = true;

    #region Components
    private CharacterController characterController;

    private TestLocomotionBehaviour locomotionBehaviour;

    private PhysicsHandler physicsHandler;
    #endregion

    public float JumpGravity {
        get { return ( 2 * data.jumpHeight ) / Mathf.Pow(data.jumpPeakTime, 2); }
    }

    public float FallGravity {
        get { return ( 2 * data.jumpHeight ) / Mathf.Pow(data.jumpFallTime, 2); }
    }

    public float JumpVelocity {
        get { return JumpGravity * data.jumpPeakTime; }
    }

    private void Start() {
        characterController = GetComponent<CharacterController>();
        physicsHandler = GetComponent<PhysicsHandler>();
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();
    }

    public override void Act<T>(T data) {
        if (physicsHandler.IsGrounded) {
            Jump();
        }
    }

    private void Update() {
        // if falling, apply fall gravity
        if (physicsHandler.velocity.y <= 0f)
            physicsHandler.gravityAccel = FallGravity;
    }

    private void Jump() {
        physicsHandler.velocity.y = JumpVelocity;
        physicsHandler.gravityAccel = JumpGravity;
    }

}
