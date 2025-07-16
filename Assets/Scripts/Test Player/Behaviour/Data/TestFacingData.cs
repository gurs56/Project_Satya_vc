using UnityEngine;

[CreateAssetMenu(fileName = nameof(TestFacingData), menuName = behaviourDataDir + nameof(TestFacingData))]
public class TestFacingData : TestBehaviourData {
    public float rotationSensitivity = 1.0f;

    /// <summary>
    /// <para>
    /// Clamps amount the <c>verticalRotationObject</c> can rotate.<br/>
    /// Where <c>x=minimum(downward) angle</c>  and <c>y=maximum(upward) angle</c>
    /// </para>
    /// <para>
    /// This field is private.<br/> 
    /// <c>GetVerticalRotationClamp</c> negates and returns this variable to make its use in-inspector more intuitive.
    /// </para>
    /// </summary>
    [SerializeField]
    private Vector2 verticalRotationClamp = new Vector2(-90,90);

    public Vector2 GetVerticalRotationClamp() { return verticalRotationClamp; }
}
