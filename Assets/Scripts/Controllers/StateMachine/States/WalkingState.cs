using UnityEngine;

public class WalkingState : IPlayerState
{
    private PlayerStateMachine player;

    public WalkingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter() { }

    public void Update()
    {
        if (!player.IsGrounded())
        {
            player.ChangeState(new FallingState(player));
            return;
        }

        if (player.IsNotMovingHorizontally())
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (player.IsJumping())
        {
            player.ChangeState(new JumpingState(player));
            return;
        }

        if (player.IsTouchingPushable())
        {
            player.ChangeState(new PushingObjectState(player));
            return;
        }

        if (player.IsTouchingRope() && player.IsMovingVertically())
        {
            player.ChangeState(new RopeClimbingState(player));
            return;
        }
    }

    public void Exit() { }
}
