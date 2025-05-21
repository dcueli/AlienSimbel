using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{

    // Recupera el sprite 2d y box collider 2d
    public Collider2D collider2D;
    public SpriteRenderer spriteRenderer;
    // Estado de la antorcha
    public bool torchState = true;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa el collider 2d
        collider2D = GetComponent<Collider2D>();
        // Inicializa el sprite 2d
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TorchOffSource"))
        {
            // TODO: Cambia el sprite a apagado 
            TorchOff();
        }
        if (collision.CompareTag("TorchOnSource"))
        {
            TorchOn();
        }
    }

    private void TorchOn()
    {
        // Aquí puedes cambiar el sprite a encendido, para el ejemplo se cambia el color a rojo
        torchState = true;
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    private void TorchOff()
    {
         // Aquí puedes cambiar el sprite a encendido, para el ejemplo se cambia el color a gris
     torchState = false;
       spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }
}
