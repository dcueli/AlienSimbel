using System;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    [Header("Jump variables")]
    [SerializeField] private float jumpHeight = 3.5f;
    [SerializeField] private float timeToJumpApex = 0.5f;
    [SerializeField] private float gravMultiplier = 1f;
    [SerializeField] private float downWardMultiplier = 2f;
    [SerializeField] private float jumpCutOff = 3f;

    
    //Movement variables
    private float _directionX;
    private Vector2 _velocity;
    Vector2 _desiredVelocity;
    private float _maxSpeedChange;
    private float _acceleration;
    private float _deceleration;
    private float _turnSpeed;
    
    //Jump variables
    private float _gravityScale;
    private float _jumpSpeed;
    private bool _currentlyJumping = false;
    
    private PlayerComponents _playerComponents;

    private void Start()
    {
        _playerComponents = GetComponent<PlayerComponents>();
        
        float gravity = (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        _gravityScale = gravity / Physics2D.gravity.y;

        _jumpSpeed = Mathf.Abs(gravity) * timeToJumpApex;
    }

    public void Move()
    {
        _directionX = _playerComponents.PlayerInput.Movement.x;
        _desiredVelocity = new Vector2(_directionX, _playerComponents.Rigidbody2D.velocity.y) * maxSpeed;
        
        _acceleration = _playerComponents.IsGrounded ? maxAcceleration : maxAirAcceleration;
        _deceleration = _playerComponents.IsGrounded ? maxDeceleration : maxAirDeceleration;
        _turnSpeed = _playerComponents.IsGrounded ? maxTurnSpeed : maxAirTurnSpeed;

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

    public void Jump()
    {
        _playerComponents.PlayerInput.JumpDesired = false;
        _currentlyJumping = true;

        Vector2 velocity = _playerComponents.Rigidbody2D.velocity;
        velocity.y = _jumpSpeed;
        _playerComponents.Rigidbody2D.velocity = velocity;
    }
    
    public void HandleJumpGravity()
    {
        Vector2 velocity = _playerComponents.Rigidbody2D.velocity;

        if (_playerComponents.IsGrounded)
        {
            _currentlyJumping = false;
        }

        if (velocity.y > 0.01f)
        {
            _currentlyJumping = true;
            gravMultiplier = _playerComponents.PlayerInput.PressingJump ? 1f : jumpCutOff;
        }
        else if (velocity.y < -0.01f)
        {
            gravMultiplier = downWardMultiplier;
        }
        else
        {
            gravMultiplier = 1f;
        }

        _playerComponents.Rigidbody2D.gravityScale = _gravityScale * gravMultiplier;
    }

    
    
    public void ClimbRope()
    {
        _playerComponents.Rigidbody2D.gravityScale = 0f;

        Vector2 input = _playerComponents.PlayerInput.Movement;

        Vector2 velocity = _playerComponents.Rigidbody2D.velocity;

        velocity.x = input.x * maxSpeed;
        velocity.y = input.y * maxSpeed;

        _playerComponents.Rigidbody2D.velocity = velocity;
    }

    public void StopGravity()
    {
        _playerComponents.Rigidbody2D.gravityScale = 0f;
    }
    public void RestoreGravity()
    {
        _playerComponents.Rigidbody2D.gravityScale = _gravityScale;
    }
}