using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class EnemyHealthUI : MonoBehaviour
{

    [SerializeField] private GameObject healthUIPrefab;
    [SerializeField] private Transform healthUIPosition;
    [SerializeField] private float visibleTime = 5;
    [SerializeField] private Canvas healthUICanvas;

    private Transform ui;
    private Image healthSlider;
    private Transform cam;
    private float lastMadeVisibleTime;

    private void Awake()
    {
        cam = Camera.main.transform;
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void Start()
    {
        if (healthUICanvas != null)
        {
            ui = Instantiate(healthUIPrefab, healthUICanvas.transform).transform;
            healthSlider = ui.GetChild(0).GetComponent<Image>();
            ui.gameObject.SetActive(false);
        }
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;
            float healthPercent = currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = healthUIPosition.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }
}
