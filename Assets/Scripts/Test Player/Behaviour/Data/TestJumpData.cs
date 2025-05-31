using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = behaviourDataDir + "JumpData")]
public class TestJumpData : TestBehaviourData {
    public float jumpHeight = 5f;
    public float jumpDistance = 1f;
    public float jumpPeakTime = .5f;
    public float jumpFallTime = .5f;

    [Tooltip("The amount of time jumping is still possible after leaving the ground.\nSet to 0 for no coyote time.")]
    public float coyoteTime = 0.5f;
}
