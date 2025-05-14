using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [Header("Ground Check Variables")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.2f; 
    [SerializeField] private float horizontalOffset = 0.4f;

    private bool onRope = false;
    public bool OnRope { get => onRope; set => onRope = value; }
    
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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rope"))
        {
            onRope = false;
        }
    }
}
