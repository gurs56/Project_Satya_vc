using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestBehaviour : MonoBehaviour {
    protected TestPlayerController controller;

    protected State state;

    private void Awake() {
        controller = GetComponent<TestPlayerController>();
    }

    protected class State {
    }
}
