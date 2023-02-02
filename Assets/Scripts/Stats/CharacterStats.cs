using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] public Stat attackDamage;
    [SerializeField] public Stat attackSpeed;
    [SerializeField] public Stat armor;

    public float currentHealth { get; private set; }

    public event System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
            OnHealthChanged(maxHealth, currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }

}
