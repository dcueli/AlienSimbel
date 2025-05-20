using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    // Variables
    // ARRAY GAMEOBJECTS
    [Header("GameObjects")]
    [SerializeField] private GameObject[] doorsToOpen;

    Collider2D _doorCollider;
    // Start is called before the first frame update

    bool isOpen = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isOpen == false)
            {
                OpenDoor();
                isOpen = true;
            }
            else
            {
                CloseDoor();
                isOpen = false;
            }  
        }
    }
    void OpenDoor()
    {
        for (int i = 0; i < doorsToOpen.Length; i++)
        {
            _doorCollider = doorsToOpen[i].GetComponent<Collider2D>();
            if (_doorCollider != null)
            {
                _doorCollider.isTrigger = true;
                // cambia color a la puerta a amarillo
                doorsToOpen[i].GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                Debug.LogError("No Collider2D found on " + doorsToOpen[i].name);
            }
        }
    }
    
    void CloseDoor()
    {
        for (int i = 0; i < doorsToOpen.Length; i++)
        {
            _doorCollider = doorsToOpen[i].GetComponent<Collider2D>();
            if (_doorCollider != null)
            {
                _doorCollider.isTrigger = false;
                // cambia color a la puerta a marron
                doorsToOpen[i].GetComponent<SpriteRenderer>().color = new Color(0.545f, 0.271f, 0.075f); // marron
                
            }
            else
            {
                Debug.LogError("No Collider2D found on " + doorsToOpen[i].name);
            }
        }
    }
}
