using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ArrestBossScript : MonoBehaviour
{
    public GameObject text;
    public bool canCollect = false;
    public string finalSceneName;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
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
        SceneManager.LoadScene(finalSceneName);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            canCollect = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
            canCollect = false;
        }
    }
}
