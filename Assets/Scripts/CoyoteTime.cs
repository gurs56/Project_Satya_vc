using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CoyoteTime {
    public float setCoyoteTime;

    public UnityEvent onCoyoteTimeExpire;

    private bool isEnabled = true;

    public bool IsExpired {
        get { return CurrentCoyoteTime <= 0 || !isEnabled; }
        private set {
            isEnabled = !value;
            if (!isEnabled)
                onCoyoteTimeExpire.Invoke();
            else
                CurrentCoyoteTime = setCoyoteTime;
        }
    }

    public float CurrentCoyoteTime {
        get; private set;
    }

    public CoyoteTime(float defaultCoyoteTime) {
        this.setCoyoteTime = defaultCoyoteTime;
        CurrentCoyoteTime = defaultCoyoteTime;
    }

    public void Reset() {
        IsExpired = false;
        CurrentCoyoteTime = setCoyoteTime;
    }

    public void Stop() {
        IsExpired = true;
        CurrentCoyoteTime = 0;
    }

    public void Update() {
        if (!IsExpired) {
            CurrentCoyoteTime -= Time.deltaTime;
            // if coyote time expired this frame
            if (CurrentCoyoteTime <= 0)
                onCoyoteTimeExpire.Invoke();
        }
    }
}