using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PhysicsCollisionHelper : MonoBehaviour {
    [SerializeField]
    private StepHelper stepHelper;

    private void Start() {
        stepHelper.setParentObject(gameObject);
        stepHelper.Start();
    }

    private void Update() {
        stepHelper.Update();
    }

    private void OnDrawGizmosSelected() {
        stepHelper.OnDrawGizmosSelected(gameObject);
    }

    [Serializable]
    class StepHelper {
        [SerializeField]
        private LayerMask groundLayers;

        [SerializeField]
        private float downVelocity;

        private PhysicsHandler physicsHandler;

        private CharacterController characterController;

        private GameObject parentObject;

        /// <summary>
        /// Allows entity to rise without having to overcome <c>downVelocity</c>
        /// </summary>
        private bool startedRising = false;

        public bool IsWithinThreshold { get; private set; } = false;

        /// <summary>
        /// <para>if the threshold ray has ever stopped sensing ground before becoming grounded</para>
        /// stops enitiy from spnapping to the ground at the bottom of falls
        /// </summary>
        private bool leftThresholdSinceGrounded = false;

        public RaycastHit thresholdRayHit;

        public RaycastHit ThresholdRayHit { get => thresholdRayHit; }

        public float RayHeight {
            get => characterController.stepOffset + characterController.skinWidth;
        }
        public void setParentObject(GameObject parentObject) { this.parentObject = parentObject; }

        public void Start() {
            physicsHandler = parentObject.GetComponent<PhysicsHandler>();
            characterController = parentObject.GetComponent<CharacterController>();

            physicsHandler.onGrounded.AddListener(() => {
                startedRising = false;
                leftThresholdSinceGrounded = false;
            });
        }

        public void Update() {
            IsWithinThreshold = Physics.Raycast(parentObject.transform.position, Vector3.down * downVelocity, out thresholdRayHit, RayHeight, groundLayers);

            BehaviourDebug.addToDebugTracking(nameof(IsWithinThreshold), IsWithinThreshold);
            BehaviourDebug.addToDebugTracking(nameof(leftThresholdSinceGrounded), leftThresholdSinceGrounded);
            BehaviourDebug.addToDebugTracking(nameof(startedRising), startedRising);

            //toggle leftThresholdSinceGrounded
            if (!IsWithinThreshold) {
                leftThresholdSinceGrounded = true;
            }

            Vector3? physVel = physicsHandler.GetVelocity(this);

            var addedVel = 0f;

            if (physVel.HasValue && physVel.Value.y > 0) {
                addedVel = downVelocity;
            }

            //if entity is trying to rise, not counting StepHelper's passive downwards velocity, the object started rising
            if (( physicsHandler.Velocity.y + addedVel ) > 0) {
                startedRising = true;
            }

            // if entity tries to rise or ray has stopped hitting the ground
            if (startedRising|| leftThresholdSinceGrounded) {
                physicsHandler.RemoveQueuedVelocity(this, false);
            }
            else {
                //if entity is airbourne, prevent coyote time from starting, and stop it if it has already started
                if (physicsHandler.IsAirbourne) {
                    physicsHandler.startCoyoteTime = false;
                    physicsHandler.GroundedCoyoteTime.Stop();

                } else {
                    physicsHandler.SetQueuedVelocity(this, new Vector3(0, -downVelocity));
                }
            }
        }

        public void OnDrawGizmosSelected(GameObject parentObject) {
            if (characterController == null)
                characterController = parentObject.GetComponent<CharacterController>();

            Gizmos.color = Color.red;
            Gizmos.DrawRay(parentObject.transform.position, ( -parentObject.transform.up ) * RayHeight);
            Gizmos.color = Color.white;
        }

    }
}
