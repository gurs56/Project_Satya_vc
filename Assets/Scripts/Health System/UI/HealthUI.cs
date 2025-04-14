using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthUI : MonoBehaviour {
    [SerializeField]
    Health health;
    [SerializeField]
    Image shadedImage;

    Material material;

    Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        slider = GetComponent<Slider>();

        material = shadedImage.materialForRendering;

        // Initialize health and maxhealth
        SetSliderMaxValue(health.MaxHealth);

        health.maxHealthUpdated.AddListener(SetSliderMaxValue);
        health.currentHealthUpdated.AddListener(SetSliderValue);
    }

    void SetSliderMaxValue(float value) {
        slider.maxValue = value;

        //update health when maxhealth is changed, as updatehealth event is not called
        slider.value = health.CurrentHealth;

        //shader.pro;
    }

    void SetSliderValue(float value, float value2) {
        slider.value = value;

        material.SetFloat("_Health_Value", value);
    }
}
