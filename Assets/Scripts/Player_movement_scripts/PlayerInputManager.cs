using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {
    [SerializeField]
    private PlayerInputActions playerInputActions;

    public UnityEvent<bool> enableStateUpdated;

    public PlayerInputActions PlayerInputActions {
        get {
            return playerInputActions;
        }
    }

    public PlayerInputActions.PlayerActions Player {
        get {
            return playerInputActions.Player;
        }
    }

    private void OnEnable() {
        playerInputActions.Enable();
        enableStateUpdated.Invoke(true);
    }

    private void OnDisable() {
        playerInputActions.Disable();
        enableStateUpdated.Invoke(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        playerInputActions = new PlayerInputActions();
    }
}
