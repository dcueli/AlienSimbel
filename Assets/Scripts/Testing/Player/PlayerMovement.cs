using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxDeceleration;
    [SerializeField] private float maxTurnSpeed;
    
    [Header("Air movement variables")]
    [SerializeField] private float maxAirAcceleration;
    [SerializeField] private float maxAirDeceleration;
    [SerializeField] private float maxAirTurnSpeed;
    
    private float _directionX;
    private Vector2 _velocity;
    Vector2 _desiredVelocity;
    private float _maxSpeedChange;
    private float _acceleration;
    private float _deceleration;
    private float _turnSpeed;
    
    private PlayerComponents _playerComponents;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
    }
    


    private void FixedUpdate()
    {
        _directionX = _playerComponents.PlayerInput.Movement.x;
        _desiredVelocity = new Vector2(_directionX, _playerComponents.Rigidbody2D.velocity.y) * maxSpeed;
        
        _acceleration = _playerComponents.isGrounded ? maxAcceleration : maxAirAcceleration;
        _deceleration = _playerComponents.isGrounded ? maxDeceleration : maxAirDeceleration;
        _turnSpeed = _playerComponents.isGrounded ? maxTurnSpeed : maxAirTurnSpeed;

        _velocity = _playerComponents.Rigidbody2D.velocity;
        float currentX = _velocity.x;
        
        if (_directionX != 0)
        {
            if (!Mathf.Approximately(Mathf.Sign(_directionX), Mathf.Sign(currentX)))
            {
                _maxSpeedChange = _turnSpeed * Time.deltaTime;
            }
            else
            {
                _maxSpeedChange = _acceleration * Time.deltaTime;
            }
        }
        else
        {
            _maxSpeedChange = _deceleration * Time.deltaTime;
        }
        _velocity.x = Mathf.MoveTowards(currentX, _desiredVelocity.x, _maxSpeedChange);
        _playerComponents.Rigidbody2D.velocity = _velocity;
    }

    private void GetComponents()
    {
        _playerComponents = GetComponent<PlayerComponents>();
    }

    
}
