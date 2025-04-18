using UnityEngine;

public class GameMenu : MonoBehaviour, IGameOptions {
  public void LoadMainMenuScn() {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV)
      Debug.Log("GameMenu > LoadMainMenuScn > Loading Scene: <Main Menu>...");
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

    GameManager.instance.scenesManager.LoadScene(Scenes.Main);
  }

  public void PlayGameScn() {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV) {
			Debug.Log($"GameMenu > PlayGameScn > GameManager.instance ({GameManager.instance})");
    }
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

		GameManager.instance.StartGame();
    GameManager.instance.scenesManager.LoadScene(Scenes.Main);
  }

  public void ExitGame() {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV)
      Debug.Log("Exitting the game...");
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

    Application.Quit();
  }
}