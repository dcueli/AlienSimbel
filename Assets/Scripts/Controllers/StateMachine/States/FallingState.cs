using UnityEngine;

public class FallingState : IPlayerState
{
    private PlayerStateMachine player;

    public FallingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter() { }

    public void Update()
    {
        if (player.IsTouchingRope() && player.IsMovingVertically())
        {
            player.ChangeState(new RopeClimbingState(player));
            return;
        }

        if (player.IsGrounded())
        {
            if (Mathf.Abs(player.rb.velocity.x) > 0.1f)
                player.ChangeState(new WalkingState(player));
            else
                player.ChangeState(new IdleState(player));
        }
    }

    public void Exit() { }
}
