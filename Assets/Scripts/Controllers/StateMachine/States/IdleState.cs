using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerStateMachine player;

    public IdleState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Idle");
    }

    public void Update()
    {
        player.PlayerComponents.PlayerActions.Move();
        if (!player.PlayerComponents.IsGrounded)
        {
            player.ChangeState(new FallingState(player));
            return;
        }

        if (Mathf.Abs(player.PlayerComponents.Rigidbody2D.velocity.x) > 0.1f)
        {
            player.ChangeState(new WalkingState(player));
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
