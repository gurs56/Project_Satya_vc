using System;
using UnityEngine.InputSystem;

public abstract class TestSustainedBehaviour<T> : TestSustainedBehaviourParent where T : struct {

    private new void Awake() {
        base.Awake();
        //DataType = typeof(T);
        controller.SustainedBehaviourDict.Add(GetType(), this);
    }

    public abstract void Act(T input);

    public override void Act<U>(U input) {
        Act((T)(object)input);
    }

    public override void ActFromInput(InputAction input) {
        Act(input.ReadValue<T>());
    }
}

public abstract class TestSustainedBehaviourParent : TestBehaviour {
    public abstract void ActFromInput(InputAction input);
}


