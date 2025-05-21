using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HudUIHandler : MonoBehaviour
{
    private Animator animator;
    private EventSystem eventSystem;
    
    [SerializeField] private GameObject pauseMenuGO;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private TMP_Text nDeathsText;

    private string timerString;

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
        nDeathsText.text = GameManager.instance.NumberDeaths.ToString()+" DEATHS";
        counterText.text = FormatTimer();
        
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

    private string FormatTimer()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.instance.Timer);

        string timeFormatted;
        if (timeSpan.Hours > 0)
        {
            timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds);
        }
        else
        {
            timeFormatted = string.Format("{0:D2}:{1:D2}",
                timeSpan.Minutes,
                timeSpan.Seconds);
        }

        return timeFormatted;
    }
}
