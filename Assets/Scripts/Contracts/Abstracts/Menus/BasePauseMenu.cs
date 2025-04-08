using UnityEngine;

abstract class BasePauseMenu : MonoBehaviour, IPauseGame {
  [SerializeField] private bool IsGamePaused = false;

  [SerializeField] protected GameObject PausePanel;

  public bool isGamePaused {
    get => IsGamePaused;
    protected set => IsGamePaused = value;
  }

  public abstract void ToogleGamePause(bool status = false);
  public abstract void ResumeGame();
  public abstract void RestartGame();
  public abstract void ExitGame();
}
