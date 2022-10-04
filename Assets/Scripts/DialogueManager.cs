using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    // public Dialogue dialogue; 

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        // FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        sentences = new Queue<string>();
        
    }


    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear(); 

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        // Debug.Log(sentence);
        dialogueText.text = sentence;

    }

    void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }

}