using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Cinemachine;

public class DialogueUI : MonoBehaviour
{
    #region Singleton
    public static DialogueUI Instance { get; private set; }

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

    [Header("UI")]
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProCustom bodyText;

    private Queue<string> sentences;
    private UnityEvent onDialogueFinishEvent;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(NpcData npcData, Dialogue dialogue)
    {
        anim.SetBool("isShown", true);
        nameText.text = npcData.name;
        nameText.color = npcData.nameColor;
        sentences.Clear();
        onDialogueFinishEvent = dialogue.onDialogueFinishEvent;
        PlayerController.Instance.isInDialogue = true;
        foreach (string sentence in dialogue.dialogueData.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        bodyText.ReadText(sentence);
    }

    private void EndDialogue()
    {
        anim.SetBool("isShown", false);
        PlayerController.Instance.isInDialogue = false;
        onDialogueFinishEvent.Invoke();
    }
}
