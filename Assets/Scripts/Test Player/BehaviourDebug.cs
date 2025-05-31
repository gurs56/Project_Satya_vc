using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviourDebug : MonoBehaviour {
    public GameObject subject;

    public TextMeshProUGUI textMeshPro;

    private PhysicsHandler physicsHandler;

    private TestJumpBehaviour testJumpBehaviour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        physicsHandler = subject.GetComponent<PhysicsHandler>();
        testJumpBehaviour = subject.GetComponent<TestJumpBehaviour>();
    }

    // Update is called once per frame
    void Update() {
        string debugString = "";

        Action<string, string> addToDebugString = (name, value) => { debugString += name + " " + value + "\n"; };

        addToDebugString.Invoke(nameof(physicsHandler.transform.position),physicsHandler.transform.position.ToString("F3"));
        addToDebugString.Invoke(nameof(physicsHandler.Velocity), physicsHandler.Velocity.ToString("F3"));
        addToDebugString.Invoke(nameof(physicsHandler.gravityAccel), physicsHandler.gravityAccel.ToString("F3"));

        textMeshPro.text = debugString;
    }
}
