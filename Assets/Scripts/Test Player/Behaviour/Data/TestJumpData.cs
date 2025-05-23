using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = behaviourDataDir + "JumpData")]
public class TestJumpData : TestBehaviourData {
    public float jumpHeight = 5f;
    public float jumpPeakTime = 1f;
    public float jumpFallTime = 1f;
    public float jumpDistance = 1f;
}
