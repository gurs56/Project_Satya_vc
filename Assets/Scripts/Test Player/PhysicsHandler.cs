using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {
    [SerializeField]
    private float defaultGravity = 1f;

    [HideInInspector]
    public float gravityAccel = 1f;

    [HideInInspector]
    public Vector3 velocity;

    private CharacterController cc;
    /// <summary>
    /// Was the character controller grounded last frame?
    /// </summary>
    private bool wasGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();
        gravityAccel = defaultGravity;
    }

    // Update is called once per frame
    void Update() {
        if (!cc.isGrounded) {
            Fall();
        } else if (wasGrounded != cc.isGrounded) {
            velocity.y = 0;
        }

        print(wasGrounded = cc.isGrounded);

        wasGrounded = cc.isGrounded;
        cc.Move(velocity * Time.deltaTime);
    }

    void Fall() {
        velocity.y -= gravityAccel;
    }
}
