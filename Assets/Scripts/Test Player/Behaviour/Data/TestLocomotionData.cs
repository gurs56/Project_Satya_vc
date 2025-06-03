using UnityEngine;

[CreateAssetMenu(fileName = "LocomotionData", menuName = behaviourDataDir + "LocomotionData")]
public class TestLocomotionData : TestBehaviourData {
    public float speed = 1f;
    [Tooltip("Speed when airbornesetDefaultSpeed")]
    public float airSpeed = 1f;
}
