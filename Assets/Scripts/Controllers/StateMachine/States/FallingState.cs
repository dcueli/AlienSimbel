using UnityEngine;

public class FallingState : IPlayerState
{
    private PlayerStateMachine player;

    public FallingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Falling");
    }

    public void Update()
    {
        player.PlayerComponents.PlayerActions.HandleJumpGravity();
        player.PlayerComponents.PlayerActions.Move();
        
        if (player.PlayerComponents.IsOnRope && player.PlayerComponents.PlayerInput.Movement.y > 0)
        {
            player.ChangeState(new RopeClimbingState(player));
            return;
        }

        if (player.PlayerComponents.IsGrounded)
        {
            if (Mathf.Abs(player.PlayerComponents.Rigidbody2D.velocity.x) > 0.1f)
                player.ChangeState(new WalkingState(player));
            else
                player.ChangeState(new IdleState(player));
        }
    }

    public void Exit() { }
}
