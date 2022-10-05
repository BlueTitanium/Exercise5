using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartPage : MonoBehaviour
{
    private void Start() 
    {
        Cursor.lockState = CursorLockMode.None; 
    }

    //// Buttons on the start page
    // start button
    public void GameStart()
    {
        SceneManager.LoadScene("Dialogue");
    
    }

    // settings button
    public void GameSettings()
    {

    }

    // quit button
    public void GameQuit()
    {
        Application.Quit();
    }

    ////////////
    // Settings Control 
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }



}
