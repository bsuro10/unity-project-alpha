using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]

public class StatBarUI : MonoBehaviour
{
    [SerializeField] private Image healthSlider;
    [SerializeField] private Image manaSlider;

    private void Awake()
    {
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float maxHealth, float currentHealth)
    {
        float healthPercent = currentHealth / maxHealth;
        healthSlider.fillAmount = healthPercent;
    }
}
