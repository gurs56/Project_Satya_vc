using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TestJumpBehaviour : TestInstataneousBehaviour {
    [SerializeField]
    private TestJumpData data;

    [SerializeField]
    private GroundDetector groundDetector;

    private bool readyToJump = true;


    private CharacterController characterController;

    private TestLocomotionBehaviour locomotionBehaviour;

    public float JumpGravity {
        get {
            return 2 * data.jumpHeight / Mathf.Pow(data.jumpPeakTime, 2);
        }
    }

    public float FallGravity {
        get {
            return 2 * data.jumpHeight / Mathf.Pow(data.jumpFallTime, 2);
        }
    }

    public float JumpVelocity {
        get {
            return JumpGravity * data.jumpPeakTime;
        }
    }

    private void Start() {
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();
    }

    public override void Act() {
        print("jumped");
    }

    private void Update() {

    }

}
