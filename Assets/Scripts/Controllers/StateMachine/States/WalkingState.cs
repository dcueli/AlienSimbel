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

        if (Mathf.Abs(player.rb.velocity.x) < 0.1f)
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.ChangeState(new JumpingState(player));
            return;
        }

        if (player.IsTouchingPushable())
        {
            player.ChangeState(new PushingObjectState(player));
            return;
        }

        if (player.IsTouchingRope() && Input.GetAxisRaw("Vertical") > 0)
        {
            player.ChangeState(new RopeClimbingState(player));
            return;
        }
    }

    public void Exit() { }
}
