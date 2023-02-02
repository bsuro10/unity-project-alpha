using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public event System.Action OnAttack;
    public bool inCombat { get; private set; }
    public bool isAttacking { get; private set; }

    private CharacterStats stats;
    private CharacterStats opponentStats;
    private float attackCooldown = 0f;
    private float combatCooldown = 5f;
    private float lastAttackTime;

    void Start()
    {
        stats = gameObject.GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;
            if (OnAttack != null)
                OnAttack();
            attackCooldown = (1f / stats.attackSpeed.GetValue()) + (0.1f * stats.attackSpeed.GetValue());
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(stats.attackDamage.GetValue());
        if (opponentStats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }

}
