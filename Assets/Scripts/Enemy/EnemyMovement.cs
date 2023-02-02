using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void WalkToDestination(Vector3 point)
    {
        agent.SetDestination(point);
        RotateToTarget(point);
    }

    private void RotateToTarget(Vector3 point)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(point - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, Time.deltaTime * 5f);
    }

}