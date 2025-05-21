using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    private Animator animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void ToMainMenu()
    {
        animator.SetTrigger("ToMainMenu");
    }

    public void ToSettingsMenu()
    {
        animator.SetTrigger("ToSettingsMenu");
    }

    public void ToLoadMenu()
    {
        animator.SetTrigger("ToLoadMenu");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
