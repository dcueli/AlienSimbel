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
        player.rb.velocity = new Vector2(player.rb.velocity.x, 10f); // Valor de salto, ajusta si quieres
    }

    public void Update()
    {
        if (player.isFalling())
        {
            player.ChangeState(new FallingState(player));
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
