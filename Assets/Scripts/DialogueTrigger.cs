using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isTriggered = false ;

    public void TriggerDialogue()
    {   
        // when first clicked; start the dialogue
        if(!isTriggered)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            isTriggered = true; 
        }
        // after the first round, show the next line
        else
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        
    }


} 
