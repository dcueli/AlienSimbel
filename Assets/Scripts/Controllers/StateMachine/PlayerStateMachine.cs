using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Settings")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public LayerMask pushableLayer;
    public LayerMask ropeLayer;

    private IPlayerState currentState;

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    // Helpers públicos para estados
    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
    }

    public bool IsTouchingPushable()
    {
        return Physics2D.OverlapCircle(transform.position, 0.5f, pushableLayer);
    }

    public bool IsTouchingRope()
    {
        return Physics2D.OverlapCircle(transform.position, 0.5f, ropeLayer);
    }
    public bool IsMovingHorizontally()
    {
      return   Mathf.Abs(rb.velocity.x) > 0.1f;
    }
    public bool IsMovingVertically()
    {
        return Input.GetAxisRaw("Vertical") > 0;
    }
    public bool IsJumping()
    {
        return Input.GetButtonDown("Jump");
    }
    public bool IsNotMovingHorizontally()
    {
        return Mathf.Abs(rb.velocity.x) < 0.1f;
    }
    public bool isFalling()
    {
        return rb.velocity.y < 0;
    }
    public bool IsNotMovingVertically()
    {
        return Input.GetAxisRaw("Vertical") <= 0;
    }
}
