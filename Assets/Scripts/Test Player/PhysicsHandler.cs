using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {
    private const float gravityAccel = 9.81f;

    private float gravityScalar = 1f;

    private CharacterController cc;

    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        Fall();

        cc.velocity.Set(velocity.x, velocity.y, velocity.z);
    }

    void Fall() {
        velocity.y -= gravityScalar * gravityAccel;
    }
}
