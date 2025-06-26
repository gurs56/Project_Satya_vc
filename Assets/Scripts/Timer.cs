using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    public float maxTime = 1f;

    public bool deleteOnTimeout = true;

    public UnityEvent onTimeout;

    public UnityEvent<float> onTick;

    public float currentTime { get; private set; } = 0f;

    public void StartTimer() {
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update() {
        var trueDelta = Time.deltaTime;

        bool queueTimeout = false;
        bool queueTick = false;
        if (currentTime > 0) {
            var tempCurrentTime = currentTime - Time.deltaTime;

            // get diff between when timer should have ended and when it actually did
            if (tempCurrentTime < 0) {
                trueDelta = Time.deltaTime + tempCurrentTime;
                queueTimeout = true;
            }

            currentTime -= trueDelta;
            queueTick = true;
        } else if (queueTimeout) {
            onTimeout.Invoke();
            if (deleteOnTimeout) {
                Destroy(this);
            }
        }

        if (queueTick) { 
            onTick.Invoke(trueDelta);
        }
    }
}
