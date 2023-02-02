using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance { get; private set; }

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

    [SerializeField] private LayerMask playerLayerMask;

    public bool isInDialogue;

    private PlayerMovement playerMovement;
    private Interactable focus;

    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(1) && !isInDialogue)
        {
            RaycastHit hit;
            Ray screenPointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenPointRay, out hit, Mathf.Infinity, ~playerLayerMask))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                } 
                else
                {
                    playerMovement.WalkToDestination(hit.point);
                    RemoveFocus();
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isInDialogue)
        {
            DialogueUI.Instance.DisplayNextSentence();
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            playerMovement.FollowTarget(focus);
        }

        focus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        playerMovement.StopFollowingTarget();
    }

}
