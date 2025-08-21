using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = behaviourDataDir + "JumpData")]
public class TestJumpData : TestBehaviourData {
    public float jumpHeight = 5f;
    public float jumpDistance = 7f;
    public float jumpPeakProportion = .5f;
}
