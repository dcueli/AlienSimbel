using System;
using UnityEngine;

public class BeetleKey : MonoBehaviour, IPickable
{
    public void AddToInventory()
    {
        InventoryManager.AddKey(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AddToInventory();
            
        }
    }
}