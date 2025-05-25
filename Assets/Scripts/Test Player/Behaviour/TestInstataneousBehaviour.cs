public abstract class TestInstataneousBehaviour : TestBehaviour {
    protected new void Awake() {
        base.Awake();
        controller.InstantaneousBehaviourSet.Add(this);
    }
    public abstract void Act();
}
