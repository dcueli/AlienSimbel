using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject startSpawnPoint;
    [SerializeField] private GameObject endSpawnPoint;
    
    private GameObject player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (GameManager.instance.NextSpawnPosition == SpawnPosition.START)
        {
            player.transform.position = startSpawnPoint.transform.position;
        }
        else
        {
            player.transform.position = endSpawnPoint.transform.position;
        }
    }


    public void RespawnPlayer()
    {
        SpawnPlayer();
    }
}
