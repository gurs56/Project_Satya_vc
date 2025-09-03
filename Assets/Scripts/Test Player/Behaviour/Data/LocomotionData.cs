using UnityEngine;

[CreateAssetMenu(fileName = nameof(LocomotionData), menuName = behaviourDataDir + nameof(LocomotionData))]
public class LocomotionData : BehaviourData {
    public float speed = 1f;
    [Tooltip("Speed when airbornesetDefaultSpeed")]
    public float airSpeed = 1f;
}
