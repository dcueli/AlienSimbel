using UnityEngine;

public class PushingObjectState : IPlayerState
{
    private PlayerStateMachine player;

    public PushingObjectState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter() { }

    public void Update()
    {
        if (!player.IsTouchingPushable())
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (Mathf.Abs(player.rb.velocity.x) < 0.1f)
        {
            player.ChangeState(new IdleState(player));
            return;
        }
    }

    public void Exit() { }
}
