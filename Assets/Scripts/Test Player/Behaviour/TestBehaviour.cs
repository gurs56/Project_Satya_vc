using System;
using UnityEngine;

public abstract class TestBehaviour : MonoBehaviour {
    protected TestController controller;

    protected void Awake() {
        controller = GetComponent<TestController>();
    }

    //call act with no inputData
    public void Act() {
        Act<object>(null);
    }

    public abstract void Act<T>(T input);
}
