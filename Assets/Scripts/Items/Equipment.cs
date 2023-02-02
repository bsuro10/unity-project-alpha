using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/New Equipment")]
public class Equipment : Item
{
    // TODO: Make modifiers as stats
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public float armorModifier;
    public float damageModifier;
    public float attackSpeedModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot
{
    Neck,
    Chest,
    Gloves,
    Belt,
    Shoulders,
    Legs,
    Weapon,
    Shield,
    Feet
}
