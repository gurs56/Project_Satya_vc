using UnityEngine;

public class TestRotationBehaviour : ATestBehaviour<Vector2> {
    public TestRotationData data;

    [SerializeField]
    private Transform verticalRotationObject;

    private Vector2 rot;

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

        rot += diff;

        // Apply rotation to transforms
        var vertRot = verticalRotationObject.eulerAngles;
        var horizRot = transform.eulerAngles;

        vertRot.x = rot.x;
        horizRot.y = rot.y;

        transform.eulerAngles = horizRot;
        verticalRotationObject.eulerAngles = vertRot;
    }

    private Vector2 GetRotation() {
        return transform.eulerAngles;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rot = GetRotation();
    }
}
