using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class GroundDetector : MonoBehaviour {
    public UnityEvent onGrounded;

    private bool isGrounded = false;

    private int groundCollisions = 0;

    public bool IsGrounded { 
        get => isGrounded; 
        private set{
            isGrounded = value;
            if (isGrounded) { 
                onGrounded.Invoke();
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        IsGrounded = true;
        groundCollisions++;
    }

    private void OnCollisionExit(Collision collision) {
        groundCollisions--;
        if (groundCollisions == 0) {
            IsGrounded = false;
        }
    }

    private void Awake() {
        GetComponent<Collider>().isTrigger = true;
    }
}
