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
        player.rb.gravityScale = 0f;
        player.rb.velocity = Vector2.zero;
    }

    public void Update()
    {
        if (!player.IsTouchingRope())
        {
            player.rb.gravityScale = 1f;
            player.ChangeState(new FallingState(player));
            return;
        }

        float verticalInput = Input.GetAxisRaw("Vertical");
        player.rb.velocity = new Vector2(0, verticalInput * 3f); // Velocidad de subida/bajada

        if (Input.GetButtonDown("Jump"))
        {
            player.rb.gravityScale = 1f;
            player.ChangeState(new JumpingState(player));
            return;
        }

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f && player.IsGrounded())
        {
            player.rb.gravityScale = 1f;
            player.ChangeState(new WalkingState(player));
            return;
        }

        if (player.IsGrounded() && verticalInput <= 0)
        {
            player.rb.gravityScale = 1f;
            player.ChangeState(new IdleState(player));
        }
    }

    public void Exit()
    {
        player.rb.gravityScale = 1f;
    }
}
