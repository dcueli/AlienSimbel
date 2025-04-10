using Scenes.CameraTesting.Camara;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump variables")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float timeToJumpApex = 0.5f;
    [SerializeField] private float gravMultiplier = 1f;
    [SerializeField] private float downWardMultiplier = 2f;

    private PlayerComponents _playerComponents;
    private float _gravityScale;
    private float _jumpSpeed;
    private bool _currentlyJumping = false;
    [SerializeField] private float jumpCutOff;

    //Cambio de YDamping en la camara
    private float _fallSpeedYDampingChangeThreshold;

    void Start()
    {
        _playerComponents = GetComponent<PlayerComponents>();

        float gravity = (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        _gravityScale = gravity / Physics2D.gravity.y;

        _jumpSpeed = Mathf.Abs(gravity) * timeToJumpApex;

        _fallSpeedYDampingChangeThreshold = CameraManager.Instance.fallSpeedYDampingChangeThreshold;

    }
    
    void FixedUpdate()
    {
        Vector2 velocity = _playerComponents.Rigidbody2D.velocity;
        if (_playerComponents.isGrounded)
        {
            _currentlyJumping = false;
        }
        
        if (_playerComponents.PlayerInput.JumpDesired && _playerComponents.isGrounded)
        {
            DoAJump();
        }


        if (_playerComponents.Rigidbody2D.velocity.y > 0.01f)
        {
            _currentlyJumping = true;
            if (_currentlyJumping && _playerComponents.PlayerInput.PressingJump)
            {
                gravMultiplier = 1f;
            }
            else
            {
                gravMultiplier = jumpCutOff;
            }
        }else if (velocity.y < 0.01f) 
        {
            gravMultiplier = downWardMultiplier;
        }
        else
        {
            gravMultiplier = 1f;
        }

        _playerComponents.Rigidbody2D.gravityScale = _gravityScale * gravMultiplier;
        
        
        //Region manejo CameraYDamping
        if (_playerComponents.Rigidbody2D.velocity.y < _fallSpeedYDampingChangeThreshold &&
            !CameraManager.Instance.IsLerpingYDamping && !CameraManager.Instance.LerpedFromPlayerFalling)
        {
            CameraManager.Instance.LerpYDamping(true);
        }

        if (_playerComponents.Rigidbody2D.velocity.y >= 0f && !CameraManager.Instance.IsLerpingYDamping &&
            CameraManager.Instance.LerpedFromPlayerFalling)
        {
            CameraManager.Instance.LerpedFromPlayerFalling = false;
            
            CameraManager.Instance.LerpYDamping(false);
        }
        
    }

    private void DoAJump()
    {
        _playerComponents.PlayerInput.JumpDesired = false;
        _currentlyJumping = true;
        _playerComponents.Rigidbody2D.velocity = new Vector2(_playerComponents.Rigidbody2D.velocity.x, _jumpSpeed);
    }
}