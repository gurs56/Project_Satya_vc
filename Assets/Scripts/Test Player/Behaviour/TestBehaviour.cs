using System;
using UnityEngine;

public abstract class TestBehaviour : MonoBehaviour {
    protected TestPlayerController controller;

    //public State state;

    protected void Awake() {
        controller = GetComponent<TestPlayerController>();
    }

    //private void OnDisable() {
    //    if (state.resetOnDisable)
    //        ResetState();
    //}

    //public void ResetState() {
    //    state.Reset();
    //}

    //[Serializable]
    //public class State {
    //    public bool resetOnDisable = true;

    //    [SerializeField]
    //    private TestBehaviourData defaultState;

    //    private TestBehaviourData currentState;

    //    public T GetDefaultState<T>() where T:TestBehaviourData { return (T) defaultState; }

    //    public T GetState<T>() where T : TestBehaviourData { return (T) currentState; }

    //    public State() {
    //        currentState = defaultState;
    //    }

    //    public void Reset() {
    //        currentState = defaultState;
    //    }
    //}
}
