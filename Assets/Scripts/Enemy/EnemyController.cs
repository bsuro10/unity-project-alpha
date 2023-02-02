using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMovement), typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10f;

    private Transform target;
    private NavMeshAgent agent;
    private EnemyMovement enemnyMovement;
    private CharacterCombat enemyCombat;

    void Start()
    {
        target = PlayerController.Instance.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemnyMovement = gameObject.GetComponent<EnemyMovement>();
        enemyCombat = gameObject.GetComponent<CharacterCombat>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(target.position, transform.position);
        if (distanceFromPlayer <= lookRadius)
        {
            enemnyMovement.WalkToDestination(target.position);
            if (distanceFromPlayer <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                    enemyCombat.Attack(targetStats);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
