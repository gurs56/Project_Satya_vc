using UnityEngine;

[CreateAssetMenu(fileName = nameof(DashData), menuName = behaviourDataDir + nameof(DashData))]
public class DashData : BehaviourData {
    public float dashDuration = 3.0f;
    public float dashDistance = 1.0f;
    public AnimationCurve dashCurve;

    public float GetDashAverage() {
        return dashCurve.Integrate() / dashCurve.length;
    }
}
