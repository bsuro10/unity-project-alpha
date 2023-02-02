using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] protected AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;
    protected Animator anim;
    protected CharacterCombat combat;
    protected CharacterStats stats;
    protected AnimatorOverrideController overrideController;

    [SerializeField] private AnimationClip replaceableAnimation;

    private NavMeshAgent navMeshAgent;

    protected virtual void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        combat = gameObject.GetComponent<CharacterCombat>();
        stats = gameObject.GetComponent<CharacterStats>();
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    protected virtual void FixedUpdate()
    {
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        anim.SetFloat("speed", speed, 0.1f, Time.deltaTime);
        anim.SetFloat("attackSpeed", Mathf.Max(1, stats.attackSpeed.GetValue()));
        anim.SetBool("inCombat", combat.inCombat);
    }

    protected virtual void OnAttack()
    {
        anim.SetTrigger("attack");
        int attackAnimIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAnimation.name] = currentAttackAnimSet[attackAnimIndex];
    }
}
