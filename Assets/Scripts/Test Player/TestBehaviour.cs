using UnityEngine;

public abstract class TestBehaviour : MonoBehaviour {
    protected TestPlayerController controller;
    private void Awake() {
        controller = GetComponent<TestPlayerController>();
    }
}
