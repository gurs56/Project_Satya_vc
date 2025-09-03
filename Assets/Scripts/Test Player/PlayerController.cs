using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerController : MonoBehaviour {
    PlayerInputManager input;

    LocomotionBehaviour locomotionBehaviour;
    //FacingHandler rotationBehaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        input = GetComponent<PlayerInputManager>();
        locomotionBehaviour = GetComponent<LocomotionBehaviour>();
        //rotationBehaviour = GetComponent<FacingHandler>();

        //rotationBehaviour.normalizeInputTime = false;

        JumpBehaviour testJumpBehaviour = GetComponent<JumpBehaviour>();
        DashBehaviour testDashBehaviour = GetComponent<DashBehaviour>();

        input.Player.Jump.performed += (InputAction.CallbackContext context) => { testJumpBehaviour?.Act(); } ;
        input.Player.Dash.performed += (InputAction.CallbackContext context) => { testDashBehaviour?.Act(); };
    }

    private void Update() {
        var lookInput = input.Player.Look.activeControl;
        //print(lookInput);
        //if (input.Player.Look.activeControl) { }

        locomotionBehaviour.Act(input.Player.Move.ReadValue<Vector2>());
        if (lookInput != null) {
            //rotationBehaviour.Act(input.Player.Look.ReadValue<Vector2>());
        }
    }
}
