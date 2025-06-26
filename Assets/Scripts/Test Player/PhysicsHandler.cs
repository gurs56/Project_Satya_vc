using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {

    [Tooltip("The grace period where the entity is considered to not be airborne.\nSet to 0 for no coyote time.")]
    [SerializeField]
    private CoyoteTime groundedCoyoteTime;

    public CoyoteTime GroundedCoyoteTime {
        get => this.groundedCoyoteTime;
        private set => this.groundedCoyoteTime =  value ;
    }

    [SerializeField]
    private GroundDetector groundDetector;

    public GroundDetector GroundDetector {
        get => this.groundDetector;
    }

    /// <summary>
    /// Invoked when <c>Velocity.y</c> switches from being >= 0 to < 0.
    /// </summary>
    public UnityEvent onFallingStart;

    /// <summary>
    /// The deltaTime counted as in air.
    /// Different from <c>onFallingStart</c> in that <c>onFallingStart</c> begins immediately upon falling, whereas <c>onAirborneBegin</c> begins when <c>groundedCoyoteTime</c> <= 0.
    /// </summary>
    public UnityEvent onAirborneBegin;

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
    private Dictionary<string, Vector3> velocityDict = new();

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
            baseVelocity.y = 0f;
            groundedCoyoteTime.Reset();
        });

        if (!GroundDetector.IsGrounded) { 
            onFallingStart.Invoke();
        }
    }

    // Update is called once per frame
    void Update() {
        // Velocity that's recieved from sustained sources
        var sustainedVelocity = Vector3.zero;
        foreach (var vel in velocityDict) {
            sustainedVelocity += vel.Value;
        }

        while (joltQueue.Count > 0)
            baseVelocity += joltQueue.Dequeue();

        if (!IsGrounded)
            Fall();

        Velocity = baseVelocity + sustainedVelocity;

        cc.Move(Velocity * Time.deltaTime);

        if (Velocity.y <= 0f && OldVelocity.y > 0)
            onFallingStart.Invoke();

        OldVelocity = Velocity;
    }

    void Fall() {
        groundedCoyoteTime.Update();
        if(GroundedCoyoteTime.IsExpired)
            baseVelocity.y -= gravityAccel * Time.deltaTime;
    }

    public void AddJolt(Vector3 velocity) {
        joltQueue.Enqueue(velocity);
    }

    public void SetQueuedVelocity(object key, Vector3 velocity) {
        velocityDict[key.ToString()] = velocity;
    }

    public void RemoveQueuedVelocity(object key) {
        velocityDict.Remove(key.ToString());
    }
}
