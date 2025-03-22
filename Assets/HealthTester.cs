using UnityEngine;

public class HealthTester : MonoBehaviour {

    [SerializeField]
    Health health;

    float difference = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        doInput(KeyCode.UpArrow, difference);
        doInput(KeyCode.DownArrow, -difference);

    }
    void doInput(KeyCode keyCode, float dif) {
        if (Input.GetKeyDown(keyCode)) {
            var oldh = health.CurrentHealth;
            health.CurrentHealth += dif;
            printhealth(oldh, dif);
        }
    }

    void printhealth(float? old = null, float? dif =null) {
        string c = "";
        if (old != null) {
            c += "Old Health: " + old + "\n";
        }
        if (dif != null) {
            c += "Differece: " + dif + "\n";
        }
        c += "Current health: " + health.CurrentHealth;

        print(c);
    }

    public void Died() {
        print("died");
    }
    public void healthChanged(float value) {
        printhealth();
    }
    public void healed(float value) {
        printhealth(value, calcoldhealth(value));
    }
    public void damaged(float value) {
        printhealth(value, calcoldhealth(value));
    }

    public float calcoldhealth(float dif) {
        return health.CurrentHealth - dif;
    }
}

