using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {
    #region Inspector Variables
    [Tooltip("The grace period where the entity is considered to not be airborne.\nSet to 0 for no coyote time.")]
    [SerializeField]
    private CoyoteTime groundedCoyoteTime;

    public CoyoteTime GroundedCoyoteTime {
        get => this.groundedCoyoteTime;
        private set => this.groundedCoyoteTime = value;
    }
    /// <summary>
    /// Whether to reset <c>GroundedCoyoteTime</c> when the entity next leaves the ground.
    /// </summary>
    public bool startCoyoteTime = true;

    [SerializeField]
    private GroundDetector groundDetector;

    public GroundDetector GroundDetector {
        get => this.groundDetector;
    }
    /// <summary>
    /// <para>Invoked when the entity becomes airborne after being grounded.</para>
    /// <para>Counts <c>GroundedCoyoteTime</c> as being grounded. Use <c>GroundDetector.onAirbourneBegin</c> to ignore <c>GroundedCoyoteTime</c>.</para>
    /// </summary>
    public UnityEvent onAirbourneBegin;

    /// <summary>
    /// Invoked when <c>Velocity.y</c> switches from being >= 0 to < 0.
    /// </summary>
    public UnityEvent onFallingStart;

    #endregion

    #region Kinematics variables
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
    #endregion

    private StepDownHelper stepDownHelper;

    public bool IsGrounded {
        get { return GroundDetector.IsGrounded; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();
        stepDownHelper = GetComponent<StepDownHelper>();

        var groundDetectorCollider = GroundDetector.GetComponent<SphereCollider>();

        GroundDetector.onGrounded.AddListener(() => {
            baseVelocity.y = 0f;

            var groundDetectorBottom = groundDetector.transform.position.y - groundDetectorCollider.radius;

            var groundHeight = transform.position.y - groundDetectorBottom;

            //move down to ground + character controller skin height
            transform.position -= new Vector3(0, groundHeight - cc.skinWidth, 0);

            GroundedCoyoteTime.Stop();
            startCoyoteTime = true;
        });

        GroundDetector.onAirbourneBegin.AddListener(() => {
            if (stepDownHelper.IsWithinTreshold) {
                startCoyoteTime = false;
            }

            if(startCoyoteTime)
                GroundedCoyoteTime.Reset();
            else {
                onAirbourneBegin.Invoke();
            }
        });

        GroundedCoyoteTime.onCoyoteTimeExpire.AddListener(() => {
            if (!IsGrounded) {
                onAirbourneBegin.Invoke();
            }
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

        GroundedCoyoteTime.Update();

        BehaviourDebug.addToDebugTracking(nameof(GroundedCoyoteTime.IsExpired), GroundedCoyoteTime.IsExpired);
        BehaviourDebug.addToDebugTracking(nameof(GroundedCoyoteTime.CurrentCoyoteTime), GroundedCoyoteTime.CurrentCoyoteTime);

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
        if (GroundedCoyoteTime.IsExpired)
            baseVelocity.y -= gravityAccel * Time.deltaTime;
    }

    public void AddJolt(Vector3 velocity) {
        joltQueue.Enqueue(velocity);
    }

    #region Getters and Setters
    public void SetQueuedVelocity(object key, Vector3 velocity) {
        velocityDict[key.ToString()] = velocity;
    }

    public void RemoveQueuedVelocity(object key) {
        velocityDict.Remove(key.ToString());
    }
    public Vector3 GetVelocity(object key) {
        return velocityDict[key.ToString()];
    }
    #endregion
}
