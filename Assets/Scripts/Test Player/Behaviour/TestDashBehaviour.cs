using UnityEngine;

[RequireComponent(typeof(PhysicsHandler))]
public class TestDashBehaviour : TestInstataneousBehaviour {
    [SerializeField]
    TestDashData dashData;

    PhysicsHandler physicsHandler;

    Timer timer;

    float currentSpeedMultiplier = 0;

    private float GetDashSpeedMultiplier() {
        return 1.0f;
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();
    }

    public override void Act<T>(T data) {
        setupTimer();
    }

    void setupTimer() {
        timer = gameObject.AddComponent<Timer>();
        timer.maxTime = dashData.dashCurve.length;
        timer.deleteOnTimeout = true;
        timer.onTick.AddListener(OnTimerTick);
        timer.onTimeout.AddListener(OnTimerTimeout);
    }

    void OnTimerTick(float delta) {
        currentSpeedMultiplier = dashData.dashCurve.Evaluate(timer.currentTime);
    }

    private void Update() {
        if (currentSpeedMultiplier > 0) {
            physicsHandler.SetQueuedVelocity(this, currentSpeedMultiplier * transform.forward);
        }
    }

    void OnTimerTimeout() {
        physicsHandler.RemoveQueuedVelocity(this);
    }
}
