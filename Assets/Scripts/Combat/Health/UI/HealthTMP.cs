using TMPro;
using UnityEngine;

public class HealthTMP : MonoBehaviour {

    [SerializeField]
    Health health;

    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        text = GetComponent<TextMeshProUGUI>();

        UpdateText();

        health.currentHealthUpdated.AddListener(UpdateCurrentHealth);
        health.maxHealthUpdated.AddListener(UpdateMaxHealth);
    }

    void UpdateMaxHealth(float v) {
        UpdateText();
    }

    void UpdateCurrentHealth(float value, float value2) {
        UpdateText();
    }

    void UpdateText() {
        text.text = HealthText.GetText(health);
    }
}
