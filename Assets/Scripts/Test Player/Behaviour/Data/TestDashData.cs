using UnityEngine;

[CreateAssetMenu(fileName = nameof(TestDashData), menuName = behaviourDataDir + nameof(TestDashData))]
public class TestDashData : TestBehaviourData {
    public float dashDistance = 1.0f;
    public AnimationCurve dashCurve;

    public float GetDashAverage() {
        return dashCurve.Integrate() / dashCurve.length;
    }
}
