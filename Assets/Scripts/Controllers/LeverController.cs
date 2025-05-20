using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject[] doorsToOpen;

    [Header("Timer Settings")]
    [SerializeField] private bool hasTimer = false;
    [SerializeField] private float timerDuration = 3f;

    private bool isOpen = false;
    private Coroutine closeRoutine;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isOpen)
            {
                OpenDoor();
                isOpen = true;

                if (hasTimer)
                {
                    if (closeRoutine != null) StopCoroutine(closeRoutine);
                    closeRoutine = StartCoroutine(AutoCloseAfterDelay());
                }
            }
            else if (!hasTimer) // Solo permite cerrar manualmente si no tiene temporizador
            {
                CloseDoor();
                isOpen = false;
            }
        }
    }

    void OpenDoor()
    {
        foreach (GameObject door in doorsToOpen)
        {
            Collider2D col = door.GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = true;
                door.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                Debug.LogError("No Collider2D found on " + door.name);
            }
        }
    }

    void CloseDoor()
    {
        foreach (GameObject door in doorsToOpen)
        {
            Collider2D col = door.GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = false;
                // naranja
                door.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f);
            }
            else
            {
                Debug.LogError("No Collider2D found on " + door.name);
            }
        }
    }

    IEnumerator AutoCloseAfterDelay()
    {
        yield return new WaitForSeconds(timerDuration);
        CloseDoor();
        isOpen = false;
    }
}
