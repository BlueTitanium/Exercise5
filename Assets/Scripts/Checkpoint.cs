using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isOn = false;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void disable()
    {
        isOn = false;
        gameObject.GetComponent<Light>().enabled = false;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
    }
    public void enable()
    {
        isOn = true;
        gameObject.GetComponent<Light>().enabled = true;
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        canvas.transform.eulerAngles = new Vector3(0, Random.Range(-16f,16f), 0);
        gameObject.GetComponent<Animation>().Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isOn == false)
        {
            var a = GameObject.FindObjectsOfType<Checkpoint>();
            foreach(var b in a)
            {
                b.disable();

            }

            GameObject.FindObjectOfType<LevelManager>().checkPoint = transform.position;
            enable();
        }
    }
}
