//using System;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;

[RequireComponent (typeof(LocomotionBehaviour))]
public class FacingHandler : MonoBehaviour {
    public Vector3 EnitiyFacingDir { get; private set; }


    bool isDirectionRelativeToLook = false;
    
    [SerializeField]
    private Transform rotationObject;

    //    //public Vector2 FacingDirection {
    //    //    get { return new Vector2(transform.eulerAngles.x, rotationObject.localEulerAngles.y); }
    //    //    set {

    //    //    }
    //    //}

    //    public bool normalizeInputTime = true;

    //    /// <summary>
    //    /// Rotates by <c>input</c> * rotation sensitivity * <c>Time.deltaTime</c>
    //    /// <para>To prevent multiplication by deltaTime, set <c>normalizeInputTime</c> to <c>false</c></para>
    //    /// </summary>
    //    /// <param name="input">The degree to which the </param>
    //    public override void Act(Vector2 input) {
    //        var diff = input * data.rotationSensitivity;

    //        if (normalizeInputTime) {
    //            diff *= Time.deltaTime;
    //        }

    //        //FacingDirection += diff;
    //    }

    private void Start() {
        
    }

    private void Update() {
        //HandleEntityFacingDir();
        if (rotationObject) {
            var rot = transform.localEulerAngles;
            rot.y = rotationObject.localEulerAngles.y;
            transform.localEulerAngles = rot;
        }
    }

    //Handles the
    private void HandleEntityFacingDir() {
        if (isDirectionRelativeToLook) {

        }
    }
}
