using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteractable : MonoBehaviour
{
    public GameObject chatPromptUI;
    public GameObject dialogueBoxUI;
    public string[] dialogueLines;

    private int currentLine = 0;
    public bool inDialogue = false;

    public bool justStartedDialogue = false;

    private TextMeshProUGUI dialogueText;

    void Start()
    {
        dialogueText = dialogueBoxUI.GetComponentInChildren<TextMeshProUGUI>();

        if (dialogueText == null)
        {
            Debug.LogError($"{gameObject.name} - No TextMeshProUGUI found in dialogueBoxUI!");
        }

        dialogueBoxUI.SetActive(false);
        chatPromptUI.SetActive(false);
    }

    void Update()
    {
        if (DialogueManager.Instance.currentDialogueNPC == this)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inDialogue)
                {
                    StartDialogue();
                }
                else if (!justStartedDialogue)
                {
                    AdvanceDialogue();
                }
            }

            if (inDialogue && Input.GetKeyDown(KeyCode.Escape))
            {
                EndDialogue();
            }
        }
    }

    void StartDialogue()
    {
        if (dialogueText == null || dialogueLines.Length == 0) return;

        Time.timeScale = 0f;
        inDialogue = true;
        justStartedDialogue = true;
        currentLine = 0;

        dialogueBoxUI.SetActive(true);
        SetPromptVisible(false);

        UpdateDialogueText();
        DialogueManager.Instance.StartDialogue(this);

        Debug.Log($"{gameObject.name} - Starting dialogue: {dialogueLines[currentLine]}");

        StartCoroutine(ClearJustStartedFlag());
    }

    IEnumerator ClearJustStartedFlag()
    {
        yield return null;
        justStartedDialogue = false;
    }

    void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            UpdateDialogueText();
        }
    }

    void EndDialogue()
    {
        Time.timeScale = 1f;
        inDialogue = false;
        dialogueBoxUI.SetActive(false);
        DialogueManager.Instance.EndDialogue();

        Debug.Log($"{gameObject.name} - Ended dialogue");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.RegisterNPC(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.UnregisterNPC(this);
            SetPromptVisible(false);
        }
    }

    public void SetPromptVisible(bool state)
    {
        if (chatPromptUI != null)
            chatPromptUI.SetActive(state);
    }

    void UpdateDialogueText()
    {
        dialogueText.text = "<size=65%><b><color=red>Press E to skip</color></b></size>\n" + dialogueLines[currentLine];
    }
}
