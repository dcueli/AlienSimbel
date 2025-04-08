using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * ================================================================================================
 * BaseGameManager
 * 	 Extends: 	 Singleton<BaseGameManager>
 *   Implements: IGameManager, IButtonsManager
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * As a Abstract base Class that provide ths persistent Singleton pattern functionality a common 
 * base of all of kind managers of the Videogame and the same time, it is a intermediary between 
 * generic Singleton<T> and specific managers classes.
 * The main purpose is reduce the duplicate codes, performance the this code organization and 
 * make easy to create new managers.
 * ================================================================================================
 */
public abstract class BaseGameManager : 
	Singleton<GameManager>, 
	IGameManager
{
  // To know what enviroment it is
  [SerializeField] public GameObject DebugMsgObj;

  protected GameMenu GameMenu;
	protected SceneInfo CurrentScene;

  public GameMenu gameMenu {
		get => GameMenu;
		protected set => GameMenu = value;    
  }
  public SceneInfo currScn {
		get => CurrentScene;
		protected set => CurrentScene = value;    
  }

  private void OnApplicationFocus(bool hasFocus) {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV)
      return;
    // When the game lose or get focus, restore mouse cursor
    if (!!!hasFocus) ToogleEnableMouse(true);
    else ToogleEnableMouse(false);
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)
  }  

  protected void ResetGame() {
    Debug.LogWarning("Back to Main menu and exiting game...");
    GameManager.instance.scenesManager.LoadScene("MainScn");

    if (null == gameMenu) {
      Application.Quit();
      return;
    }

    gameMenu.ExitGame();
    return;
  }

  /**
	 * ================================================================================================
	 * Protected Method
	 * 	 ToogleEnableMouse
   *
	 * @Parameters:
	 *		bool state, TRUE by default
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * ONLY FOR PRODUCTION ENVIROMENT
	 * This lock the mouse pointer when game is running
	 * ================================================================================================
	 */
  protected void ToogleEnableMouse(bool state = true) {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
		if(Env.Dev == Core.NODE_ENV)
			return;
			
    Cursor.visible = state;
    if (!!!state)
      Cursor.lockState = CursorLockMode.Locked;
    else
      Cursor.lockState = CursorLockMode.None;
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

  }

  /**
	 * ================================================================================================
	 * Protected Method
	 * 	 ToogleDebugMessage
   *
	 * @Parameters: none
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * It shows/hides a message (TMPro.TextMeshPro) with title: "DEBUG MODE", this tells us that 
	 * application is running on --Development Enviroment--
	 * ================================================================================================
	 */
	protected void ToogleDebugMessage() {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
		if(Env.Dev == Core.NODE_ENV)
			DebugMsgObj?.SetActive(true);

		if(Env.Pro == Core.NODE_ENV) {
			DebugMsgObj?.SetActive(false);
			ToogleEnableMouse(false);
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)
	}

  public virtual void LogErrorAndExit(string message) {
		Debug.LogError(message);
		gameMenu?.ExitGame();
		Application.Quit();
		return;
	}
}