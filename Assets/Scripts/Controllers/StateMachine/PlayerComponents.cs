using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public Rigidbody2D Rigidbody2D{get{return _rb2d;}}
    private BoxCollider2D _col2d;
    public BoxCollider2D BoxCollider2D{get{return _col2d;}}
    private CollisionCheck _collisionCheck;
    public CollisionCheck CollisionCheck{get{return _collisionCheck;}}
    private PlayerInput _playerInput;
    public PlayerInput PlayerInput{get{return _playerInput;}}
    
    
    public bool isGrounded;

    
    
    // Start is called before the first frame update
    void Awake()
    {
        GetComponents();
    }

    private void FixedUpdate()
    {
        isGrounded = _collisionCheck.IsGrounded(_col2d);
    }

    private void GetComponents()
    {
        _collisionCheck = GetComponent<CollisionCheck>();
        _rb2d = GetComponent<Rigidbody2D>();
        _col2d = GetComponent<BoxCollider2D>();
        _playerInput = GetComponent<PlayerInput>();
    }
}
