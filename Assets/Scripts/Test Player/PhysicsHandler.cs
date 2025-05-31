using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {
    [SerializeField]
    private float groundedCoyoteTime = .5f;
    public float GroundedCoyoteTime { 
        get => this.groundedCoyoteTime;
        private set => this.groundedCoyoteTime =  value ;
    }

    [SerializeField]
    private GroundDetector groundDetector;

    public GroundDetector GroundDetector {
        get => this.groundDetector;
    }

    public UnityEvent onFallingStart;

    [HideInInspector]
    public float gravityAccel = 1f;

    private Vector3 velocity;
    public Vector3 Velocity { get => this.velocity; private set => this.velocity = value; }

    public Vector3 OldVelocity { get; private set; }

    private Vector3 baseVelocity;

    private CharacterController cc;

    /// <summary>
    /// Queued continuous velocity
    /// </summary>
    private Dictionary<string, Vector3> velocityQueue = new();

    /// <summary>
    /// Queued instantaneous bursts of velocity
    /// </summary>
    private Queue<Vector3> joltQueue = new Queue<Vector3>();

    public bool IsGrounded {
        get { return GroundDetector.IsGrounded; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();

        GroundDetector.onGrounded.AddListener(() => {
            baseVelocity.y = 0;
        });
    }

    // Update is called once per frame
    void Update() {
        var v = Vector3.zero;
        foreach (var vel in velocityQueue) {
            v += vel.Value;
        }

        while (joltQueue.Count > 0)
            baseVelocity += joltQueue.Dequeue();

        if (!IsGrounded)
            Fall();

        Velocity = baseVelocity + v;

        cc.Move(Velocity * Time.deltaTime);

        if (Velocity.y <= 0f && OldVelocity.y > 0)
            onFallingStart.Invoke();

        OldVelocity = Velocity;
    }

    void Fall() {
        baseVelocity.y -= gravityAccel * Time.deltaTime;
    }

    public void AddJolt(Vector3 velocity) {
        joltQueue.Enqueue(velocity);
    }

    public void SetQueuedVelocity(object key, Vector3 velocity) {
        velocityQueue[key.ToString()] = velocity;
    }
}
