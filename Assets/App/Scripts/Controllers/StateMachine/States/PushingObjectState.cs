using UnityEngine;

public class PushingObjectState : IPlayerState
{
    private PlayerStateMachine player;

    public PushingObjectState(PlayerStateMachine player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Pushing");
    }

    public void Update()
    {
        if (!player.PlayerComponents.IsOnPushable && Mathf.Abs(player.PlayerComponents.Rigidbody2D.velocity.x) < 0.1f)
        {
            player.ChangeState(new IdleState(player));
            return;
        }

        if (!player.PlayerComponents.IsOnPushable && Mathf.Abs(player.PlayerComponents.Rigidbody2D.velocity.x) >= 0.1f)
        {
            player.ChangeState(new WalkingState(player));
            return;
        }
    }

    public void Exit() { }
}
