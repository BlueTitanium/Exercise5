using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {   
        // tell what conversation to start
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
} 
