public abstract class TestInstataneousBehaviour : TestBehaviour {
    private void Awake() {
        controller.InstantaneousBehaviourSet.Add(this);
    }
    public abstract void Act();
}
