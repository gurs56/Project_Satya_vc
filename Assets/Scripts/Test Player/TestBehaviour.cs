using UnityEngine;

public abstract class TestBehaviour : MonoBehaviour {
    protected TestPlayerController controller;
    private void Awake() {
        controller = GetComponent<TestPlayerController>();

        controller.BehaviourSet.Add(this);
    }

    public abstract void Act();
}
