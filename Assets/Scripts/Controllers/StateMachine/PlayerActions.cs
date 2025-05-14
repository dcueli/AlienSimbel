using System;
using UnityEngine;

public class PlayerActions:MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float maxAcceleration = 79f;
    [SerializeField] private float maxDeceleration = 76f;
    [SerializeField] private float maxTurnSpeed = 76f;
    
    [Header("Air movement variables")]
    [SerializeField] private float maxAirAcceleration = 79f;
    [SerializeField] private float maxAirDeceleration = 76f;
    [SerializeField] private float maxAirTurnSpeed = 76f;
    
    private float _directionX;
    private Vector2 _velocity;
    Vector2 _desiredVelocity;
    private float _maxSpeedChange;
    private float _acceleration;
    private float _deceleration;
    private float _turnSpeed;
    
    private PlayerComponents _playerComponents;

    private void Start()
    {
        _playerComponents = GetComponent<PlayerComponents>();
    }

    public void Move()
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
}