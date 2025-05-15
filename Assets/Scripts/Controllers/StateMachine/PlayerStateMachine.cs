using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    
    private IPlayerState currentState;
    private PlayerComponents _playerComponents;
    public PlayerComponents PlayerComponents { get => _playerComponents;}
    
    private void Start()
    {
        _playerComponents = GetComponent<PlayerComponents>();
        ChangeState(new FallingState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    
    
}
