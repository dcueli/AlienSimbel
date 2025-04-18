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

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumpDesired = true;
            _pressingJump = true;
        }

        if (context.canceled)
        {
            _pressingJump = false;
        }
    }
}
