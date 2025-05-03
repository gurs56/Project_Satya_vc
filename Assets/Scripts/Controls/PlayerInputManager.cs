using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {
    [SerializeField]
    private PlayerInputActions playerInputActions;

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

    public InputAction Motion {
        get {
            return Player.Move;
        }
    }

    private void OnEnable() {
        playerInputActions.Enable();
    }

    private void OnDisable() {
        playerInputActions.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        playerInputActions = new PlayerInputActions();
    }


}
