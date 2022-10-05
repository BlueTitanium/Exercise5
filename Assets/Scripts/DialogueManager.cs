using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;
////////
    public Dialogue dialogue;
    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Starting the Dialogue
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

    // Display the next lines
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;

    }

    // reached the end
    void EndDialogue()
    {
        // move to stage 1 scene: update later
        // SceneManager.LoadScene("SceneOne");

        Debug.Log("End of Conversation");
    }

}
