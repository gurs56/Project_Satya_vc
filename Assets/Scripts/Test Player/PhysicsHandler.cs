using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {
    [SerializeField]
    private float defaultGravity = 1f;

    [SerializeField]
    private GroundDetector groundDetector;

    [HideInInspector]
    public float gravityAccel = 1f;

    [HideInInspector]
    public Vector3 velocity;

    public Vector3 OldVelocity { get; private set; }

    private CharacterController cc;

    public bool IsGrounded {
        get { return groundDetector.IsGrounded; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();
        gravityAccel = defaultGravity;

        groundDetector.onGrounded.AddListener(() => { velocity.y = 0; });
    }

    // Update is called once per frame
    void Update() {
        if (!IsGrounded) {
            Fall();
        }
        print(IsGrounded);

        cc.Move(velocity * Time.deltaTime);

        OldVelocity = velocity;
    }

    void Fall() {
        velocity.y -= gravityAccel*Time.deltaTime;
    }
}
