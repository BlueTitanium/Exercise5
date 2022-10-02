using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectible : MonoBehaviour
{
    public int ID = 0;
    public GameObject text;
    public bool canCollect = false;
    public Sprite picture;
    public string title;
    public string description;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        if(Inventory.collected[ID] == true)
        {
            Destroy(gameObject);
        }
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
        Inventory.collected[ID] = true;
        Inventory.pictures[ID] = picture;
        Inventory.titles[ID] = title;
        Inventory.descriptions[ID] = description;
        FindObjectOfType<GameManager>().ShowItemObtained(ID);
        Destroy(gameObject);
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
