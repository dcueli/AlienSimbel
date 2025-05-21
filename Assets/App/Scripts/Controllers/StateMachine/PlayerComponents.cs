using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    private PlayerActions _playerActions;
    public PlayerActions PlayerActions{get{return _playerActions;}}

    private float _initialGravityScale;
    public float InitialGravityScale{get{return _initialGravityScale;}}
    
    private bool _isGrounded;
    public bool IsGrounded{
        get => _isGrounded;
        set => _isGrounded = value;
    }

    private bool _isOnRope;
    public bool IsOnRope
    {
        get => _isOnRope;
        set => _isOnRope = value;
    }


    private bool _isOnPushable;

    public bool IsOnPushable
    {
        get => _isOnPushable;
        set => _isOnPushable = value;
    }
    
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        GetComponents();
    }

    private void Start()
    {
        _initialGravityScale = Rigidbody2D.gravityScale;
    }

    private void FixedUpdate()
    {
        _isGrounded = _collisionCheck.IsGrounded(_col2d);
        _isOnRope = _collisionCheck.OnRope;
        _isOnPushable = _collisionCheck.OnPushable;
    }

    private void GetComponents()
    {
        _collisionCheck = GetComponent<CollisionCheck>();
        _rb2d = GetComponent<Rigidbody2D>();
        _col2d = GetComponent<BoxCollider2D>();
        _playerInput = GetComponent<PlayerInput>();
        _playerActions = GetComponent<PlayerActions>();
    }
}
