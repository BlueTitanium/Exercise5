using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DelayedNextScene : MonoBehaviour
{
    public string nextScene = "";
    public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("CallingCard") == null && !started)
        {
            StartCoroutine(GoToNextScene());
        }
    }

    public IEnumerator GoToNextScene()
    {
        started = true;
        yield return new WaitForSeconds(.1f);
        FindObjectOfType<LevelManager>().DestroyLM();
        SceneManager.LoadScene(nextScene);
    }
}
