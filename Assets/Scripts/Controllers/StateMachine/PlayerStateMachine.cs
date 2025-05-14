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

    //SEPARAR EN CLASE DE ACCIONES Y CLASE DE CHEQUEOS
    public void Move()
    {
        
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
}
