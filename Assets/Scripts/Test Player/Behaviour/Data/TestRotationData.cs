using UnityEngine;

[CreateAssetMenu(fileName = nameof(TestRotationData), menuName = behaviourDataDir + nameof(TestRotationData))]
public class TestRotationData : TestBehaviourData {
    public float rotationSensitivity = 1.0f;
}
