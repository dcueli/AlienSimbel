using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    private List<BeetleKey> keys = new List<BeetleKey>();

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AddKey(BeetleKey key)
    {
        instance.keys.Add(key);
        Debug.Log("Current Amount of Keys: "+instance.keys.Count);
        //Conexion al HUD
        UpdateUI();
    }

    private static void UpdateUI()
    {
        //Connect with UI system
    }

    //Function for when accesing the door in Fase1-Puzzle
    public static bool CheckKeyAmount(int amountOfKeysForPuzzle)
    {
        return instance.keys.Count == amountOfKeysForPuzzle;
    }
}
