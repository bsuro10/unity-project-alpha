using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{    
    [SerializeField] public float radius = 2f;
    [SerializeField] public Transform interactionPoint;

    private bool isFocus = false;
    private bool hasInteracted = false;
    private Transform playerTransform;

    public virtual void Interact()
    {
        Debug.Log("Interacting with: " + transform.name);
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (distanceFromPlayer <= radius)
            {
                hasInteracted = true;
                Interact();
            }
        }
    }

    public void OnFocused(Transform newPlayerTransform)
    {
        isFocus = true;
        hasInteracted = false;
        playerTransform = newPlayerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
        playerTransform = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
