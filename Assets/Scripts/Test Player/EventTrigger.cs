using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A collision trigger that invokes <c>UnityEvent</c>s when it begins to collide and when it begins to stop colliding
/// </summary>
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class EventTrigger : MonoBehaviour {
    [SerializeField]
    private LayerMask collisionLayers;

    /// <summary>
    /// <para>Called when <c>EventTrigger</c> begins no longer colliding with any other valid collider.</para>
    /// </summary>
    public UnityEvent onExitAllColisions;

    /// <summary>
    /// <para>Called when <c>EventTrigger</c> starts colliding with a valid collider.</para>
    /// Not called when already colliding with another valid collider.
    /// </summary>
    public UnityEvent onStartColliding;

    //the amount of ground collisions the GroundChecker detects
    private int collisions = 0;

    private bool wasColliding = false;

    private bool isColliding = false;

    public bool IsColliding {
        get => isColliding;
        private set {
            isColliding = value;
            // if GroundChecker becomes grounded when it wasnt before, call onStartColliding event
            if (isColliding && !wasColliding) {
                onStartColliding.Invoke();
            }
            // if GroundChecker becomes airborne when it wasnt before, call onExitAllColisions event
            else if (!isColliding && wasColliding) {
                onExitAllColisions.Invoke();
            }
            wasColliding = value;
        }
    }

    private void OnTriggerEnter(Collider other) {
        IsColliding = true;
        collisions++;
    }

    private void OnTriggerExit(Collider other) {
        collisions--;
        if (collisions == 0) {
            IsColliding = false;
        }
    }

    private void Awake() {
        GetComponent<Collider>().isTrigger = true;
        var groundCollider = GetComponent<Collider>();
        var rb = GetComponent<Rigidbody>();

        rb.includeLayers = groundCollider.includeLayers = collisionLayers;
        rb.excludeLayers = groundCollider.excludeLayers = ~collisionLayers;

        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
