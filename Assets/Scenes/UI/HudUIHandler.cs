using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HudUIHandler : MonoBehaviour
{
    private Animator animator;
    private EventSystem eventSystem;
    
    [SerializeField] private GameObject pauseMenuGO;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        eventSystem = EventSystem.current;
        pauseMenuGO.SetActive(false);
        eventSystem.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuGO.activeInHierarchy)
        {
            eventSystem.enabled = true;
            animator.SetTrigger("EnterPauseMenu");
            pauseMenuGO.SetActive(true);
        }
    }

    public void ExitPauseMenu()
    {
        animator.SetTrigger("ExitPauseMenu");
    }

    public void ToPauseMenu()
    {
        animator.SetTrigger("ToPauseMenu");
    }

    public void ToSettingsMenu()
    {
        animator.SetTrigger("ToSettingsMenu");
    }

    public void DeactivateUIInput()
    {
        eventSystem.enabled = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(Scenes.Main);
    }
}
