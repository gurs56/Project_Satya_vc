using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TestFacingBehaviour : ATestBehaviour<Vector2> {
    public TestFacingData data;

    [SerializeField]
    private Transform verticalRotationObject;

    public Vector2 FacingDirection {
        get { return new Vector2(transform.eulerAngles.y, verticalRotationObject.localEulerAngles.x); }
        set {
            var hrot = transform.eulerAngles;
            hrot.y = value.x;
            transform.eulerAngles = hrot;

            var vrot = verticalRotationObject.localEulerAngles;

            var clamp = data.GetVerticalRotationClamp();
            var oldrot=value.x;
            vrot.x = Mathf.Clamp(value.y+180, clamp.y, clamp.x)-180;
            BehaviourDebug.addToDebugTracking(verticalRotationObject.localEulerAngles.x);
            verticalRotationObject.localEulerAngles = vrot;
            print("objrot=" +verticalRotationObject.localEulerAngles.x+ "\noldval="+oldrot+"\nclamp="+ clamp);
        }
    }

    public bool normalizeInputTime = true;

    /// <summary>
    /// Rotates by <c>input</c> * rotation sensitivity * <c>Time.deltaTime</c>
    /// <para>To prevent multiplication by deltaTime, set <c>normalizeInputTime</c> to <c>false</c></para>
    /// </summary>
    /// <param name="input">The degree to which the </param>
    public override void Act(Vector2 input) {
        var diff = input * data.rotationSensitivity;

        if (normalizeInputTime) {
            diff *= Time.deltaTime;
        }

        FacingDirection += diff;
    }
}
