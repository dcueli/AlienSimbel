using UnityEngine;

public class WalkingState : IPlayerState
{
    private PlayerStateMachine player;

    public WalkingState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Walking");
    }

    public void Update()
    {
        player.PlayerComponents.PlayerActions.Move();
        if (!player.PlayerComponents.IsGrounded && Mathf.Abs(player.PlayerComponents.Rigidbody2D.velocity.y)<0.1f)
        {
            player.ChangeState(new FallingState(player));
            return;
        }

        if (Mathf.Abs(player.PlayerComponents.Rigidbody2D.velocity.x) < 0.1f)
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (player.PlayerComponents.PlayerInput.JumpDesired)
        {
            player.ChangeState(new JumpingState(player));
            return;
        }

        if (player.PlayerComponents.IsOnPushable)
        {
            player.ChangeState(new PushingObjectState(player));
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
