using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class TestPlayerController : MonoBehaviour {
    PlayerInputManager input;

    public HashSet<TestInstataneousBehaviour> InstantaneousBehaviourSet {
        get;
        private set;
    } = new HashSet<TestInstataneousBehaviour>();
    public HashSet<TestBehaviour> SustainedBehaviourSet {
        get;
        private set;
    } = new HashSet<TestBehaviour>();

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

        //foreach (var item in SustainedBehaviourSet) {
        //    var a = behaviourInputActions[item.GetType()];
        //    var i = (TestSustainedBehaviour)item;

        //    item.  i.ReadValue<>();
        //}
    }
}
