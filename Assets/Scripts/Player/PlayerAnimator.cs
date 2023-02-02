using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }

    public WeaponAnimations[] weaponAnimations;

    private Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    protected override void Start()
    {
        base.Start();
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations weaponAnimation in weaponAnimations)
        {
            weaponAnimationsDict.Add(weaponAnimation.weapon, weaponAnimation.clips);
        }
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            if (weaponAnimationsDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationsDict[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipmentSlot == EquipmentSlot.Weapon)
        {
            currentAttackAnimSet = defaultAttackAnimSet;
        }
    }

}
