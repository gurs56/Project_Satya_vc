using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviourDebug : MonoBehaviour
{
    public GameObject subject;

    public TextMeshProUGUI textMeshPro;

    private PhysicsHandler physicsHandler;

    private TestJumpBehaviour testJumpBehaviour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print(textMeshPro);

        physicsHandler = subject.GetComponent<PhysicsHandler>();
        testJumpBehaviour = subject.GetComponent<TestJumpBehaviour>();
    }

    // Update is called once per frame
    void Update() {
        textMeshPro.text = physicsHandler.velocity.ToString();
    }
}
