using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = behaviourDataDir + "JumpData")]
public class JumpData : BehaviourData {
    public float jumpHeight = 5f;
    public float jumpDistance = 7f;
    public float jumpPeakProportion = .5f;
}
