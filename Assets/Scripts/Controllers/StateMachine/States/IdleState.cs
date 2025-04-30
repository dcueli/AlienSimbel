using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine player;

    public IdleState(PlayerStateMachine player)
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

        if (player.IsMovingHorizontally())
        {
            player.ChangeState(new WalkingState(player));
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
