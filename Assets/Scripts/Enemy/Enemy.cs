using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private CharacterStats enemyStats;
    private PlayerController playerController;

    void Start()
    {
        playerController = PlayerController.Instance;
        enemyStats = gameObject.GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerController.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(enemyStats);
        }
    }

}
