using System;
using UnityEngine.InputSystem;

public abstract class TestSustainedBehaviour<T> : TestSustainedBehaviourParent where T : struct {

    private new void Awake() {
        base.Awake();
        //DataType = typeof(T);
        controller.SustainedBehaviourDict.Add(GetType(), this);
    }

    public override void ActFromInput(InputAction input) {
        Act(input.ReadValue<T>());
    }

    public T GetInputData() {
        return (T)inputData;
    }

    private void Update() {
        Act();
    }
}

public abstract class TestSustainedBehaviourParent :TestBehaviour {
    public abstract void ActFromInput(InputAction input);
    //public Type DataType { get; protected set; }

    protected object inputData;

    //public void SetData<T>(T inputData) {
    //    if (typeof(T) == DataType) {
    //        this.inputData = inputData;
    //        return;
    //    }
    //    throw new TypeAccessException($"Cannot set inputData of type {DataType} to {typeof(T)}.");
    //}
}


