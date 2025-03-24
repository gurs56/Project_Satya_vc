using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthUI : MonoBehaviour {
    [SerializeField]
    Health health;

    Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        slider = GetComponent<Slider>();

        // Initialize health and maxhealth
        SetSliderMaxValue(health.MaxHealth);

        health.onMaxHealthUpdated.AddListener(SetSliderMaxValue);
        health.onCurrentHealthUpdated.AddListener(SetSliderValue);
    }

    void SetSliderMaxValue(float value) {
        slider.maxValue = value;

        //update health when maxhealth is changed, as updatehealth event is not called
        slider.value = health.CurrentHealth;
    }

    void SetSliderValue(float value) {
        slider.value = value;
    }
}
