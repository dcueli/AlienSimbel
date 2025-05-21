using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [Header("Ground Check Variables")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.2f; 
    [SerializeField] private float horizontalOffset = 0.4f;
    private LevelManager levelManager;
    
    private bool onRope = false;
    public bool OnRope { get => onRope; set => onRope = value; }
    
    private bool onPushable = false;
    public bool OnPushable { get => onPushable; set => onPushable = value; }

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    public bool IsGrounded(BoxCollider2D collider)
    {
        if (collider == null) return false;
        Vector3 colliderBase = collider.bounds.center;
        colliderBase.y = collider.bounds.min.y;

        Vector3 leftRayOrigin = colliderBase - new Vector3(horizontalOffset, 0, 0);
        Vector3 rightRayOrigin = colliderBase + new Vector3(horizontalOffset, 0, 0);
        Vector3 centerRayOrigin = colliderBase;

        bool leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, groundCheckDistance, groundLayer);
        bool rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, groundCheckDistance, groundLayer);
        bool centerHit = Physics2D.Raycast(centerRayOrigin, Vector2.down, groundCheckDistance, groundLayer);
            
        Debug.DrawRay(leftRayOrigin, Vector3.down * groundCheckDistance, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector3.down * groundCheckDistance, Color.red);
        Debug.DrawRay(centerRayOrigin, Vector3.down * groundCheckDistance, Color.red);
            
        return centerHit || leftHit || rightHit;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rope"))
        {
            onRope = true;
        }

        if (other.CompareTag("Pushable"))
        {
            onPushable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rope"))
        {
            onRope = false;
        }

        if (other.CompareTag("Pushable"))
        {
            onPushable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            levelManager.RespawnPlayer();
            GameManager.instance.NumberDeaths++;
        }
    }
}
