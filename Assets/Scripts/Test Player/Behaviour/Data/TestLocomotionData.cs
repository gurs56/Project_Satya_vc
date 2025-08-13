using UnityEngine;

[CreateAssetMenu(fileName = nameof(TestLocomotionData), menuName = behaviourDataDir + nameof(TestLocomotionData))]
public class TestLocomotionData : TestBehaviourData {
    public float speed = 1f;
    [Tooltip("Speed when airbornesetDefaultSpeed")]
    public float airSpeed = 1f;
}
