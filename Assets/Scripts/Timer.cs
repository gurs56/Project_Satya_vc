using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    /// <summary>
    /// The time the timer will be set to when started
    /// </summary>
    public float setTime;

    public UnityEvent onTick;

    /// <summary>
    /// <return>Returns the amount of time it's overshot.</return>
    /// </summary>
    public UnityEvent<float> onExpire;

    public bool IsExpired {
        get { return CurrentTime <= 0; }

        private set {
            if (value) {
                CurrentTime = 0;
                onExpire.Invoke(CurrentTime);
            } else
                CurrentTime = setTime;
        }
    }

    public float CurrentTime {
        get; private set;
    }

    public void SetTime(float setTime) {
        this.setTime = setTime;
        CurrentTime = setTime;
    }

    public void Reset() {
        IsExpired = false;
        CurrentTime = setTime;
    }

    /// <summary>
    /// Sets the timer to expired
    /// </summary>
    public void Stop() {
        IsExpired = true;
    }

    private void Update() {
        if (!IsExpired) {
            CurrentTime -= Time.deltaTime;

            onTick.Invoke();

            // if coyote time expired this frame
            if (CurrentTime <= 0)
                onExpire.Invoke(CurrentTime);
        }
    }
}