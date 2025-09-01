using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(TestLocomotionBehaviour), typeof(PhysicsHandler))]
public class TestJumpBehaviour : MonoBehaviour {
    [SerializeField]
    private TestJumpData data;

    private bool canJump = false;

    #region Components
    private TestLocomotionBehaviour locomotionBehaviour;

    private PhysicsHandler physicsHandler;
    #endregion

    public float JumpDuration {
        get {
            return data.jumpDistance / locomotionBehaviour.data.airSpeed;
        }
    }

    private bool IsGravityLessOrEqZero(out float value) {
        value = -1;
        if (JumpDuration <= 0 || data.jumpPeakProportion <= 0) {
            value = 0;
            return true;
        }

        return false;
    }

    public float JumpGravity {
        get {
            if (IsGravityLessOrEqZero(out var grav))
                return grav;

            return ( 2 * data.jumpHeight ) / Mathf.Pow(JumpDuration * data.jumpPeakProportion, 2);
        }
    }

    public float FallGravity {
        get {
            if (IsGravityLessOrEqZero(out var grav))
                return grav;

            return ( 2 * data.jumpHeight ) / Mathf.Pow(JumpDuration * ( 1 - data.jumpPeakProportion ), 2);
        }
    }

    public float JumpVelocity {
        get { return JumpGravity * JumpDuration / 2; }
    }

    #region Tracking Variables
    public Vector3 startJumpPos { get; private set; } = Vector3.zero;

    public float jumpDist { get; private set; } = 0;
    public float jumpHeight { get; private set; } = 0;
    #endregion

    public void Act() {
        if (canJump && !physicsHandler.CeilingDetector.IsColliding) {
            Jump();
        }
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();

        physicsHandler.onFallingStart.AddListener(() => {
            physicsHandler.gravityAccel = FallGravity;

            jumpHeight = transform.position.y - startJumpPos.y;
        });

        physicsHandler.GroundDetector.onStartColliding.AddListener(() => {
            canJump = true;

            jumpDist = ( new Vector2(transform.position.x, transform.position.z) - new Vector2(startJumpPos.x, startJumpPos.z) ).magnitude;

        });

        physicsHandler.onAirbourneBegin.AddListener(() => {
            canJump = false;
        });


    }

    private void Jump() {
        physicsHandler.AddJolt(new Vector3(0, JumpVelocity, 0));
        physicsHandler.gravityAccel = JumpGravity;

        physicsHandler.GroundedCoyoteTime.Stop();
        physicsHandler.startCoyoteTime = false;

        startJumpPos = transform.position;
    }

//    private void Update() {
//#if UNITY_EDITOR
//        BehaviourDebug.addToDebugTracking(nameof(JumpDuration), JumpDuration);
//        BehaviourDebug.addToDebugTracking(nameof(jumpHeight), jumpHeight);
//        BehaviourDebug.addToDebugTracking(nameof(jumpDist), jumpDist);
//#endif
//    }
}
