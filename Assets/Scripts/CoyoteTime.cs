using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CoyoteTime {
    public float defaultCoyoteTime;

    public UnityEvent onCoyoteTimeExpire;

    private bool isEnabled = true;

    public bool IsExpired {
        get { return CurrentCoyoteTime <= 0 || !isEnabled; }
        set {
            isEnabled = !value;
            if (!isEnabled)
                onCoyoteTimeExpire.Invoke();
            else
                CurrentCoyoteTime = defaultCoyoteTime;
        }
    }

    public float CurrentCoyoteTime {
        get; private set;
    }

    public CoyoteTime(float defaultCoyoteTime) {
        this.defaultCoyoteTime = defaultCoyoteTime;
        CurrentCoyoteTime = defaultCoyoteTime;
    }

    public void Reset() {
        IsExpired = false;
        CurrentCoyoteTime = defaultCoyoteTime;
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