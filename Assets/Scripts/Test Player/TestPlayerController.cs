using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class TestPlayerController : TestController {
    PlayerInputManager input;

    TestLocomotionBehaviour locomotionBehaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        input = GetComponent<PlayerInputManager>();
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();
        TestJumpBehaviour testJumpBehaviour = GetComponent<TestJumpBehaviour>();
        TestDashBehaviour testDashBehaviour = GetComponent<TestDashBehaviour>();

        input.Player.Jump.performed += (InputAction.CallbackContext context) => { testJumpBehaviour?.Act(); } ;
        input.Player.Dash.performed += (InputAction.CallbackContext context) => { testDashBehaviour?.Act(); };
    }

    private void Update() {
        locomotionBehaviour.Act(input.Player.Move.ReadValue<Vector2>());
    }
}
