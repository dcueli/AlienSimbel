using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicActivator : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private PlayerInput otherInput;
    [SerializeField] private bool playerOnTop;

    private void Start()
    {
        MimicActivationInputManager.RegisterActivator(this);
    }

    private void OnDestroy()
    {
        MimicActivationInputManager.UnregisterActivator(this);
    }

    public bool TryActivate()
    {
        if (playerOnTop && input.enabled)
        {
            input.enabled = false;
            otherInput.enabled = true;

            return true; 
        }

        return false; 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnTop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnTop = false;
        }
    }
}
