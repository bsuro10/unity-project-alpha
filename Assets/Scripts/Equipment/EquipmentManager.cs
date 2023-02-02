using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public struct EquipmentSkinnedMeshRenderer
{
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer skinnedMeshRenderer;
}

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public EquipmentSkinnedMeshRenderer[] playerMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private Equipment[] currentEquipment;
    private SkinnedMeshRenderer[] currentMeshes;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int) newItem.equipmentSlot;
        Equipment oldEquipment = UnEquip(slotIndex);
        
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(newItem, oldEquipment);

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        SkinnedMeshRenderer parentSkinnedMeshRenderer = Array.Find(playerMeshes, e => e.equipmentSlot == newItem.equipmentSlot).skinnedMeshRenderer;
        newMesh.transform.parent = parentSkinnedMeshRenderer.transform;
        newMesh.bones = parentSkinnedMeshRenderer.bones;
        newMesh.rootBone = parentSkinnedMeshRenderer.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment UnEquip(int slotIndex)
    {
        Equipment equipment = currentEquipment[slotIndex];
        SkinnedMeshRenderer meshRenderer = currentMeshes[slotIndex];
        if (equipment != null)
        {
            if (meshRenderer != null)
                Destroy(meshRenderer.gameObject);

            inventoryManager.Add(equipment);
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, equipment);
        }
        return equipment;
    }

    public void UnEquipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
    }

    // TODO: For testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnEquipAll();
        }
    }
}
