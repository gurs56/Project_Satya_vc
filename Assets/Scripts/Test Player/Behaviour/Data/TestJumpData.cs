using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = behaviourDataDir + "JumpData")]
public class TestJumpData : TestBehaviourData {
    public float jumpHeight = 5f;
    public float jumpDistance = 1f;
    public float jumpPeakTime = .5f;
    public float jumpFallTime = .5f;
}
