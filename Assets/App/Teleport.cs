using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private SpawnPosition nextSpawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.NextSpawnPosition = nextSpawnPosition;
            SceneManager.LoadScene(sceneName);
        }
        
    }
}

public enum SpawnPosition
{
    START,
    END
}
