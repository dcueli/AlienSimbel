using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedTorchOnExample : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torch") && collision.GetComponent<TorchController>().torchState == false)
        {
            // Aquí puedes cambiar el sprite a encendido, para el ejemplo se cambia el color a rojo
           Debug.Log("Torch is off, you need to turn it on!");
        }else if (collision.CompareTag("Torch") && collision.GetComponent<TorchController>().torchState == true)
        {
            // Aquí puedes cambiar el sprite a encendido, para el ejemplo se cambia el color a rojo
           Debug.Log("Torch is on, you can proceed!");
        }
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
