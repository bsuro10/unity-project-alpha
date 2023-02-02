using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{

    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = InventoryManager.Instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }

}
