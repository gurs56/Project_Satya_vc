using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(TestLocomotionBehaviour), typeof(PhysicsHandler))]
public class TestJumpBehaviour : ATestBehaviour<object> {
    [SerializeField]
    private TestJumpData data;

    private bool CanJump = false;

    #region Components
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

    public override void Act(object data) {
        if (CanJump) {
            Jump();
        }
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();

        physicsHandler.onFallingStart.AddListener(() => {
            physicsHandler.gravityAccel = FallGravity;
        });

        physicsHandler.GroundDetector.onGrounded.AddListener(() => { CanJump = true; });
        physicsHandler.GroundedCoyoteTime.onCoyoteTimeExpire.AddListener(() => { CanJump = false; });
    }

    private void Jump() {
        physicsHandler.AddJolt(new Vector3(0, JumpVelocity, 0));
        physicsHandler.gravityAccel = JumpGravity;
        physicsHandler.GroundedCoyoteTime.IsExpired = true;
    }

}
