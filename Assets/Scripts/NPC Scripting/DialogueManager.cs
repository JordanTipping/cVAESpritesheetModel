using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private List<NPCInteractable> nearbyNPCs = new List<NPCInteractable>();
    public NPCInteractable currentDialogueNPC;

    private Transform player;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (nearbyNPCs.Count > 0)
        {
            NPCInteractable closest = null;
            float minDistance = Mathf.Infinity;

            foreach (var npc in nearbyNPCs)
            {
                float dist = Vector2.Distance(player.position, npc.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = npc;
                }
            }

            currentDialogueNPC = closest;

            foreach (var npc in nearbyNPCs)
            {
                npc.SetPromptVisible(npc == closest && currentDialogueNPC.inDialogue == false);
            }
        }
        else
        {
            currentDialogueNPC = null;
        }
    }


    public void RegisterNPC(NPCInteractable npc)
    {
        if (!nearbyNPCs.Contains(npc))
            nearbyNPCs.Add(npc);
    }

    public void UnregisterNPC(NPCInteractable npc)
    {
        if (nearbyNPCs.Contains(npc))
            nearbyNPCs.Remove(npc);
    }

    public void StartDialogue(NPCInteractable npc)
    {
        currentDialogueNPC = npc;
        foreach (var other in nearbyNPCs)
        {
            if (other != npc)
                other.SetPromptVisible(false);
        }
    }

    public void EndDialogue()
    {
        currentDialogueNPC = null;
    }
}
