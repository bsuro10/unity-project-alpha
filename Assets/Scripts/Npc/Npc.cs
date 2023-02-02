using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{

    [SerializeField] private NpcData npcData;
    [SerializeField] private Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();
        if (npcData != null && dialogue != null)
            DialogueUI.Instance.StartDialogue(npcData, dialogue);
    }

}
