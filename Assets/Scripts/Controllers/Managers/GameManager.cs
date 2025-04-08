using Unity.VisualScripting;
using UnityEngine;

/**
 * ================================================================================================
 * GameManager
 *
 * Extends: BaseGameManager
 * Implements: None
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * Main Manager of the Videogame, it means, a "Central access point" to main logic of the 
 * videogame, data and functionality that must be globally reachable trought all of Scenes
 * ================================================================================================
 */
public class GameManager : BaseGameManager{
	[SerializeField] private ScenesManager ScenesManager;
	public ScenesManager scenesManager {
		get => ScenesManager;
		set => ScenesManager = value;
	}

	private bool _bGameStarted = false;
	public bool bGameStarted {
		get => _bGameStarted;
		set => _bGameStarted = value;
	}

  // Initialize and start the Videogame
  private new void Awake() {
      base.Awake();
			SetInitialProperties();

			// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
			#pragma warning disable CS0162 
			if(Env.Dev == Core.NODE_ENV) {
				Debug.Log($"GameManager > Awake > GetInstanceID::({GetInstanceID()})");
				Debug.Log($"GameManager > Awake > currScn.name::({currScn.name})");
			}
			#pragma warning restore CS0162
			// END::@dcueli -> enable warning CS0162 (unrecheable code)

			if (Scenes.Main == currScn.name)
				StartGame();
  }

	private void SetInitialProperties() {
		// Althougt the below line may be not necessary and just in case the compiler show warning about 
		// GameManager object order in hierarchy
		// I leave this next code. Just to be sure GameManage is in first place on Hierarchy tree
		transform.SetParent(null);
		// Show/Hide the message tell us if the APP is running in the Development enviroment
		ToogleDebugMessage();
		// Set GameMenu Object in <gameMenu> property because will need to build the menu options of both
		// "Main Menu" and "Track selector Menu" and "Pause Menu"
    gameMenu = FindObjectOfType<GameMenu>();
		currScn = ScenesManager.GetCurrentSceneInfo() ?? null;
			
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162
    if(Env.Dev == Core.NODE_ENV) {
			Debug.Log($"");
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)
	}

  public void StartGame() {
		bGameStarted = true;
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
    if(Env.Dev == Core.NODE_ENV) {
      Debug.Log($"GameManager > StartGame > The games has started :{bGameStarted})");
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

		// 
		// Here, implemente your business logic
		// 
  }
}