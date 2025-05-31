using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(TestLocomotionData), typeof(PhysicsHandler))]
public class TestJumpBehaviour : TestInstataneousBehaviour {
    [SerializeField]
    private TestJumpData data;

    private float currentCoyoteTime = 0f;

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
    public override void Act<T>(T data) {
        // No need to check if grounded, coyote time will always be max if grounded. 
        if (currentCoyoteTime > 0) {
            Jump();
        }
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();

        currentCoyoteTime = data.coyoteTime;

        // reset coyote time when grounded
        physicsHandler.GroundDetector.onGrounded.AddListener(() => {
            currentCoyoteTime = data.coyoteTime;
        });

        physicsHandler.onFallingStart.AddListener(() => {
            physicsHandler.gravityAccel = FallGravity;
        });
    }


    private void Update() {
        if (!physicsHandler.IsGrounded && currentCoyoteTime > 0)
            currentCoyoteTime -= Time.deltaTime;
    }

    private void Jump() {
        physicsHandler.AddJolt(new Vector3(0, JumpVelocity, 0));
        physicsHandler.gravityAccel = JumpGravity;
    }
}
