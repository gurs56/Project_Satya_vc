using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class GroundDetector : MonoBehaviour {
    [SerializeField]
    private LayerMask groundLayers;

    public UnityEvent onAirbourneBegin;

    public UnityEvent onGrounded;

    //the amount of ground collisions the GroundChecker detects
    private int groundCollisions = 0;

    private bool wasGrounded = false;

    private bool isGrounded = false;

    public bool IsGrounded {
        get => isGrounded;
        private set {
            isGrounded = value;
            // if GroundChecker becomes grounded when it wasnt before, call onGrounded event
            if (isGrounded && !wasGrounded) {
                onGrounded.Invoke();
            }
            // if GroundChecker becomes airborne when it wasnt before, call onAirbourneBegin event
            else if (!isGrounded && wasGrounded) {
                onAirbourneBegin.Invoke();
            }
            wasGrounded = value;
        }
    }

    private void OnTriggerEnter(Collider other) {
        IsGrounded = true;
        groundCollisions++;
    }

    private void OnTriggerExit(Collider other) {
        groundCollisions--;
        if (groundCollisions == 0) {
            IsGrounded = false;
        }
    }

    private void Awake() {
        GetComponent<Collider>().isTrigger = true;
        var groundCollider = GetComponent<Collider>();
        var rb = GetComponent<Rigidbody>();

        rb.includeLayers = groundCollider.includeLayers = groundLayers;
        rb.excludeLayers = groundCollider.excludeLayers = ~groundLayers;

        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
