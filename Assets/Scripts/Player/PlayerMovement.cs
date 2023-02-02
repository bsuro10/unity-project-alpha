using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotateSpeedMovement = 0.075f;

    private NavMeshAgent navMeshAgent;
    private float rotateVelocity;
    private Transform targetToFollow;

    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (targetToFollow != null)
        {
            WalkToDestination(targetToFollow.position);
        }
    }

    public void WalkToDestination(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
        RotateToTarget(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        targetToFollow = newTarget.interactionPoint;
        navMeshAgent.stoppingDistance = newTarget.radius * .5f;
    }

    public void StopFollowingTarget()
    {
        targetToFollow = null;
        navMeshAgent.stoppingDistance = 0;
    }

    private void RotateToTarget(Vector3 point)
    {
        /*Quaternion rotationToLookAt = Quaternion.LookRotation(point - transform.position);
        float rotationY = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref rotateVelocity,
            rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);*/
        Quaternion rotationToLookAt = Quaternion.LookRotation(point - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, rotateSpeedMovement * Time.deltaTime * 5f);
    }
}
