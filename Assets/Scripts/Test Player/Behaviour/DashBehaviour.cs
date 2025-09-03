using UnityEngine;

[RequireComponent(typeof(PhysicsHandler))]
public class DashBehaviour : MonoBehaviour {
    [SerializeField]
    DashData dashData;

    [SerializeField]
    Timer timer;

    PhysicsHandler physicsHandler;


    float currentSpeedMultiplier = 0;

    public void Act() {
        timer.Reset();
    }

    private void Start() {
        physicsHandler = GetComponent<PhysicsHandler>();

        //Setup timer
        timer.SetTime(dashData.dashCurve.length);
        timer.onExpire.AddListener(OnTimerTimeout);
    }

    void OnTimerTick(float delta) {
        currentSpeedMultiplier = dashData.dashCurve.Evaluate(timer.CurrentTime);
    }

    private void Update() {
        if (currentSpeedMultiplier > 0) {
            physicsHandler.SetQueuedVelocity(this, currentSpeedMultiplier * transform.forward);
        }
    }

    void OnTimerTimeout(float overtime) {
        physicsHandler.RemoveQueuedVelocity(this);
    }
}
