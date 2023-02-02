using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    private void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            attackDamage.AddModifier(newItem.damageModifier);
            attackSpeed.AddModifier(newItem.attackSpeedModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            attackDamage.RemoveModifier(oldItem.damageModifier);
            attackSpeed.RemoveModifier(oldItem.attackSpeedModifier);
        }
    }

    public override void Die()
    {
        Debug.Log("Player died.");
    }

}
