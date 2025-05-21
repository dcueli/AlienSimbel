using UnityEngine;

public class RopeClimbingState : IPlayerState
{
    private PlayerStateMachine player;

    public RopeClimbingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("RopeClimbing");
        player.PlayerComponents.PlayerActions.StopGravity();
    }

    public void Update()
    {
        player.PlayerComponents.PlayerActions.ClimbRope();
        
        if (player.PlayerComponents.PlayerInput.Movement.x != 0)
        {
            player.ChangeState(new FallingState(player));
            return;
        }

        
        if (player.PlayerComponents.PlayerInput.JumpDesired)
        {
            player.ChangeState(new JumpingState(player));
            return;
        }

        if (player.PlayerComponents.IsGrounded && player.PlayerComponents.PlayerInput.Movement.x != 0)
        {
            player.ChangeState(new WalkingState(player));
            return;
        }

        if (player.PlayerComponents.IsGrounded && player.PlayerComponents.PlayerInput.Movement.x == 0)
        {
            player.ChangeState(new IdleState(player));
        }
    }

    public void Exit()
    {
        player.PlayerComponents.PlayerActions.RestoreGravity();
    }
}
