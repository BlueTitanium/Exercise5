using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CourthouseHandler : MonoBehaviour
{
    public int count = 0;
    public TextMeshProUGUI countText;
    public GameObject firstScreen;
    public GameObject secondScreen;
    public GameObject results;
    public TextMeshProUGUI resultText;
    public GameObject dialogueScreenWeak;
    public GameObject dialogueScreenMid;
    public GameObject dialogueScreenStrong;
    // Start is called before the first frame update
    void Start()
    {
        firstScreen.SetActive(true);
        secondScreen.SetActive(false);
        dialogueScreenWeak.SetActive(false);
        dialogueScreenMid.SetActive(false);
        dialogueScreenStrong.SetActive(false);
        for (int i = 0; i < Inventory.collected.Length; i++)
        {
            if(Inventory.collected[i] == true)
            {
                count++;
            }
        }
        countText.text = ""+count;
        if(count <= 4)
        {
            resultText.text = "NOT GUILTY";
        } else if (count <= 8)
        {
            resultText.text = "LIGHT SENTENCE";
        } else
        {
            resultText.text = "LIFE IMPRISONMENT";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSecondScreen()
    {
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
    }

    public void showResults()
    {
        if (count <= 4)
        {
            dialogueScreenWeak.SetActive(true);
        }
        else if (count <= 8)
        {
            dialogueScreenMid.SetActive(true);
        }
        else
        {
            dialogueScreenStrong.SetActive(true);
        }
    }

}
