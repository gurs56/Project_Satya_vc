using UnityEngine;

[CreateAssetMenu(fileName = "LocomotionData", menuName = behaviourDataDir + "LocomotionData")]
public class TestLocomotionData : TestBehaviourData {
    public float speed = 1f;
    [Tooltip("The speed whe not grounded.")]
    public float airSpeed = 1f;
}
