using UnityEngine;

public class JumpingState : IPlayerState
{
    private PlayerStateMachine player;

    public JumpingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Jumping");
        player.PlayerComponents.PlayerActions.Jump();
    }

    public void Update()
    {
        player.PlayerComponents.PlayerActions.HandleJumpGravity();
        player.PlayerComponents.PlayerActions.Move();
        
        if (player.PlayerComponents.Rigidbody2D.velocity.y < 0)
        {
            player.ChangeState(new FallingState(player));
            return;
        }

        if (player.PlayerComponents.IsOnRope && player.PlayerComponents.PlayerInput.Movement.y > 0)
        {
            player.ChangeState(new RopeClimbingState(player));
            return;
        }
    }

    public void Exit() { }
}
