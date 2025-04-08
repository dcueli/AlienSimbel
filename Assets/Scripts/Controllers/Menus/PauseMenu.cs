using UnityEngine;

class PauseMenu : BasePauseMenu {
  // Update is called once per frame
  private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape))
      ToogleGamePause(true);
  }

  public override void ToogleGamePause(bool status = false) {
    isGamePaused = status;

    if (isGamePaused) {
      Time.timeScale = 0;
      PausePanel.SetActive(true);
    } else {
      Time.timeScale = 1f;
      PausePanel.SetActive(false);
    }
  }

  public override void ResumeGame() {
    ToogleGamePause(false);
  }

  public override void RestartGame() {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV)
      Debug.Log("PauseMenu > RestartGame > Volviendo al Menú Principal...");
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

    ToogleGamePause(false);
    ScenesManager.Load(Scenes.Main);
  }

  public override void ExitGame() {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV)
      Debug.Log("Saliendo del juego...");
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

    ToogleGamePause(false);
    Application.Quit();
  }
}
