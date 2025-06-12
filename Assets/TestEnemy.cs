using UnityEngine;

public class TestEnemy : MonoBehaviour {
    [SerializeField]
    float speed = 1f;

    CharacterController cc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        var v = new Vector3(Mathf.Sin(Time.time), 0, 0) * speed;
        cc.Move(v);
    }
}
