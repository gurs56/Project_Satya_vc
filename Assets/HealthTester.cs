using UnityEngine;

public class HealthTester : MonoBehaviour {

    [SerializeField]
    Health health;

    float difference = 15f;

    float oldhealth = 0;

    // Update is called once per frame
    void Update() {
        oldhealth = health.CurrentHealth;
        doInput(KeyCode.UpArrow, difference, false);
        doInput(KeyCode.DownArrow, -difference, false);

        doInput(KeyCode.RightArrow, difference, true);
        doInput(KeyCode.LeftArrow, -difference, true);
    }
    void doInput(KeyCode keyCode, float dif, bool maxhealth) {
        if (Input.GetKeyDown(keyCode)) {
            if (maxhealth) {
                health.MaxHealth += dif;
            } else { 
                health.CurrentHealth += dif;
            }
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
        print("health changed");
        printhealth();
    }
    public void healed(float value) {
        print("healed");
        printhealth(oldhealth, value);
    }
    public void damaged(float value) {
        print("damaged");
        printhealth(oldhealth, value);
    }

    public void maxHealthChanged(float value) {
        print("maxhealth: " + value);
    }
}

