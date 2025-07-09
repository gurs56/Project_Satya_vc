using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(PlayerInputManager))]
public class TestPlayerController : TestController {
    PlayerInputManager input;

    TestLocomotionBehaviour locomotionBehaviour;
    TestRotationBehaviour rotationBehaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        input = GetComponent<PlayerInputManager>();
        locomotionBehaviour = GetComponent<TestLocomotionBehaviour>();
        rotationBehaviour = GetComponent<TestRotationBehaviour>();

        rotationBehaviour.normalizeInputTime = false;

        TestJumpBehaviour testJumpBehaviour = GetComponent<TestJumpBehaviour>();
        TestDashBehaviour testDashBehaviour = GetComponent<TestDashBehaviour>();

        input.Player.Jump.performed += (InputAction.CallbackContext context) => { testJumpBehaviour?.Act(); } ;
        input.Player.Dash.performed += (InputAction.CallbackContext context) => { testDashBehaviour?.Act(); };
    }

    private void Update() {
        print(input.Player.Look.activeControl);
        //if (input.Player.Look.activeControl) { }

        locomotionBehaviour.Act(input.Player.Move.ReadValue<Vector2>());
        rotationBehaviour.Act(input.Player.Look.ReadValue<Vector2>());
    }
}
