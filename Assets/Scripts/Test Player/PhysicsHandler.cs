using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PhysicsHandler : MonoBehaviour {
    #region Inspector Variables
    [Tooltip("The grace period where the entity is considered to not be airborne.\nSet to 0 for no coyote time.")]
    [SerializeField]
    private Timer groundedCoyoteTime;

    public Timer GroundedCoyoteTime {
        get => this.groundedCoyoteTime;
        private set => this.groundedCoyoteTime = value;
    }

    [SerializeField]
    private EventTrigger groundDetector;

    public EventTrigger GroundDetector {
        get => this.groundDetector;
    }

    [SerializeField]
    private EventTrigger ceilingDetector;

    public EventTrigger CeilingDetector {
        get => ceilingDetector;
    }

    #region Events
    public UnityEvent onHeadBump {
        get => CeilingDetector.onStartColliding;
    }

    public UnityEvent onGrounded {
        get => GroundDetector.onStartColliding;
    }

    /// <summary>
    /// <para>Invoked when the entity becomes airborne after being grounded.</para>
    /// <para>Counts <c>GroundedCoyoteTime</c> as being grounded. Use <c>EventTrigger.onExitAllColisions</c> to ignore <c>GroundedCoyoteTime</c>.</para>
    /// </summary>
    public UnityEvent onAirbourneBegin;

    /// <summary>
    /// Invoked when <c>Velocity.y</c> switches from being >= 0 to < 0 while <c>IsAirbourne</c>.
    /// </summary>
    public UnityEvent onFallingStart;
    #endregion

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

    /// <summary>
    /// <para>Whether to reset <c>GroundedCoyoteTime</c> when the entity next leaves the ground.</para>
    /// Resets upon becoming <c>IsColliding</c>
    /// </summary>
    [HideInInspector]
    public bool startCoyoteTime = true;

    public bool IsGrounded {
        get;
        private set;
    } = false;

    public bool IsAirbourne {
        get { return !IsGrounded; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();

        var groundDetectorCollider = GroundDetector.GetComponent<SphereCollider>();

        onGrounded.AddListener(() => {
            baseVelocity.y = 0f;

            var groundDetectorBottom = groundDetector.transform.position.y - groundDetectorCollider.radius;

            var groundHeight = transform.position.y - groundDetectorBottom;

            //move down to ground + character controller skin height
            transform.position -= new Vector3(0, groundHeight - cc.skinWidth, 0);

            GroundedCoyoteTime.Stop();
            startCoyoteTime = true;
            IsGrounded = true;
        });

        GroundDetector.onExitAllColisions.AddListener(() => {
            if (startCoyoteTime) {
                GroundedCoyoteTime.Reset();

            } else {
                onAirbourneBegin.Invoke();
            }
        });

        GroundedCoyoteTime.onExpire.AddListener((float overtime) => {
            if (!GroundDetector.IsColliding) {
                onAirbourneBegin.Invoke();
            }
        });

        onAirbourneBegin.AddListener(() => {
            IsGrounded = false;
        });

        if (!GroundDetector.IsColliding) {
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

#if UNITY_EDITOR
        DoDebug();
#endif
        TryFalling();

        Velocity = baseVelocity + sustainedVelocity;

        if(new Vector2(Velocity.x, Velocity.z).magnitude == 0) {
            GroundedCoyoteTime.Stop();
        }

        cc.Move(Velocity * Time.deltaTime);

        // if velocity becomes negative and the entity is airbourne, count as falling
        if (Velocity.y <= 0f && OldVelocity.y > 0 && IsAirbourne)
            onFallingStart.Invoke();

        OldVelocity = Velocity;
    }

    void TryFalling() {
        if (GroundedCoyoteTime.IsExpired && !IsGrounded)
            baseVelocity.y -= gravityAccel * Time.deltaTime;
    }

    #region Velocity Exposers
    public void AddJolt(Vector3 velocity) {
        joltQueue.Enqueue(velocity);
    }

    public void SetQueuedVelocity(object key, Vector3 velocity) {
        velocityDict[key.ToString()] = velocity;
    }

    public void RemoveQueuedVelocity(object key, bool warnIfNull = true) {
        if (velocityDict.ContainsKey(key.ToString()))
            velocityDict.Remove(key.ToString());

        if (warnIfNull)
            Debug.LogWarning($"No such key \"{key.ToString()}\" in queued velocity.");
    }
    public Vector3? GetVelocity(object key) {
        if (velocityDict.ContainsKey(key.ToString()))
            return velocityDict[key.ToString()];
        return null;
    }
    #endregion

    #region Debugging
#if UNITY_EDITOR
    private void DoDebug() {
        BehaviourDebug.addToDebugTracking(nameof(IsGrounded), IsGrounded);
        BehaviourDebug.addToDebugTracking(nameof(GroundedCoyoteTime.IsExpired), GroundedCoyoteTime.IsExpired);
        BehaviourDebug.addToDebugTracking(nameof(GroundedCoyoteTime.CurrentTime), GroundedCoyoteTime.CurrentTime);
    }
#endif
    #endregion
}
