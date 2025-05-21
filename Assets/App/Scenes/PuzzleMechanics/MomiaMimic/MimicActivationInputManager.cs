using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MimicActivationInputManager : MonoBehaviour
{
    private static MimicActivationInputManager instance;
    private List<MimicActivator> activators = new List<MimicActivator>();
    private bool isInputLocked = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInputLocked)
        {
            foreach (var activator in activators)
            {
                if (activator.TryActivate()) 
                {
                    isInputLocked = true;
                    StartCoroutine(UnlockInputAfterDelay());
                    break; 
                }
            }
        }
    }

    private IEnumerator UnlockInputAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isInputLocked = false;
    }

    public static void RegisterActivator(MimicActivator activator)
    {
        if (instance != null && !instance.activators.Contains(activator))
        {
            instance.activators.Add(activator);
        }
    }

    public static void UnregisterActivator(MimicActivator activator)
    {
        if (instance != null && instance.activators.Contains(activator))
        {
            instance.activators.Remove(activator);
        }
    }
}