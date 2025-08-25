using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(CharacterController))]
public class StepDownHelper : MonoBehaviour {
    [SerializeField]
    private LayerMask groundLayers;

    private CharacterController characterController;

    public bool IsWithinTreshold(out RaycastHit hitInfo) { 
        return Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo, characterController.stepOffset, groundLayers);
    }

    private void Start() {
        characterController = GetComponent<CharacterController>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * characterController.stepOffset);
        Gizmos.color = Color.white;
    }
}
