using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(TestLocomotionBehaviour), typeof(PhysicsHandler))]
public class TestJumpBehaviour : MonoBehaviour {
    [SerializeField]
    private TestJumpData data;

    private bool CanJump = false;

    #region Components
    private TestLocomotionBehaviour locomotionBehaviour;

    private PhysicsHandler physicsHandler;
    #endregion

    public float JumpDuration {
        get {
            return data.jumpDistance/ locomotionBehaviour.data.airSpeed;
        }
    }

    public float JumpGravity {
        get { return ( 2 * data.jumpHeight ) / Mathf.Pow(JumpDuration * data.jumpPeakProportion, 2); }
    }

    public float FallGravity {
        get { return ( 2 * data.jumpHeight ) / Mathf.Pow(JumpDuration * ( 1 - data.jumpPeakProportion ), 2); }
    }

    public float JumpVelocity {
        get { return JumpGravity * JumpDuration / 2; }
    }

    private Vector3 startJumpPos = Vector3.zero;

    private float jumpDist = 0;
    private float jumpHeight = 0;

    public void Act() {
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

        physicsHandler.GroundDetector.onGrounded.AddListener(() => {
            CanJump = true;

            jumpDist = ( new Vector2(transform.position.x, transform.position.z) - new Vector2(startJumpPos.x, startJumpPos.z) ).magnitude;
        });

        physicsHandler.onAirbourneBegin.AddListener(() => {
            CanJump = false;
        });

        physicsHandler.onFallingStart.AddListener(() => {
            jumpHeight = transform.position.y - startJumpPos.y;
        });
    }

    private void Jump() {
        physicsHandler.AddJolt(new Vector3(0, JumpVelocity, 0));
        physicsHandler.gravityAccel = JumpGravity;

        physicsHandler.GroundedCoyoteTime.Stop();
        physicsHandler.startCoyoteTime = false;

        startJumpPos = transform.position;
    }

    private void Update() {
        BehaviourDebug.addToDebugTracking(nameof(JumpDuration), JumpDuration);
        BehaviourDebug.addToDebugTracking(nameof(jumpHeight), jumpHeight);
        BehaviourDebug.addToDebugTracking(nameof(jumpDist), jumpDist);
    }
}
