using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class TestPlayerController : TestController {
    PlayerInputManager input;

    // Dictionarry connecting inputactions to behaviours
    private Dictionary<Type, InputAction> behaviourInputActions = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        input = GetComponent<PlayerInputManager>();

        behaviourInputActions = new Dictionary<Type, InputAction> {
            {typeof(TestJumpBehaviour), input.Player.Jump},
            {typeof(TestDashBehaviour), input.Player.Dash},
            {typeof(TestLocomotionBehaviour), input.Player.Move }
        };

        foreach (var item in InstantaneousBehaviourSet) {
            behaviourInputActions[item.GetType()].performed += (InputAction.CallbackContext context) => { item.Act(); };
        }
    }

    private void Update() {
        foreach (var item in SustainedBehaviourDict) {
            item.Value.ActFromInput(behaviourInputActions[item.Key]);
        }
    }
}
