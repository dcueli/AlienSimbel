using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerInput : MonoBehaviour,PlayerControls.ILocomotionMapActions
{
    private PlayerControls _playerControls;

    private Vector2 _movement;
    public Vector2 Movement{ get => _movement; }

    private bool _jumpDesired;
    public bool JumpDesired
    {
        get => _jumpDesired;
        set => _jumpDesired = value;
    }
    
    private bool _pressingJump ;
    public bool PressingJump
    {
        get => _pressingJump;
        set => _pressingJump = value;
    }
    
    [Header("Jump Input Buffer")]
    [SerializeField] private float jumpInputBufferTime = 0.2f; // Tiempo del buffer
    private float _jumpInputBufferCounter = 0f;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        
        _playerControls.Enable();
        
        _playerControls.LocomotionMap.Enable();
        _playerControls.LocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        _playerControls.LocomotionMap.RemoveCallbacks(this);
        _playerControls.LocomotionMap.Disable();
        
        _playerControls.Disable();
    }

    private void Update()
    {
        if (_jumpInputBufferCounter > 0)
        {
            _jumpInputBufferCounter -= Time.deltaTime;
            _jumpDesired = true;
        }
        else
        {
            _jumpDesired = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _pressingJump = true;
            _jumpInputBufferCounter = jumpInputBufferTime;
        }

        if (context.canceled)
        {
            _pressingJump = false;
        }
    }
}
