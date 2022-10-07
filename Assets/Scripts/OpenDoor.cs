using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class OpenDoor : MonoBehaviour
{

    public int IDToOpen = 1;
    public GameObject text;
    public GameObject disabledText;
    public bool canCollect = false;
    public string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        disabledText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canCollect && Input.GetMouseButtonDown(1))
        {
            collect();
        }
    }
    public void collect()
    {
        FindObjectOfType<LevelManager>().DestroyLM();
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Inventory.collected[IDToOpen])
        {
            text.SetActive(true);
            canCollect = true;
        } else if (other.gameObject.CompareTag("Player") && !Inventory.collected[IDToOpen])
        {
            disabledText.SetActive(true);
            canCollect = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
            disabledText.SetActive(false);
            canCollect = false;
        }
    }
}
