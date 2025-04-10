using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * ================================================================================================
 * BaseScenesManager
 * 	 Extends: 	 Singleton<BaseScenesManager>
 *   Implements: IGameManager, IButtonsManager
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * As a Abstract base Class that provide ths persistent Singleton pattern functionality a common 
 * base of all of kind managers of the Videogame and the same time, it is a intermediary between 
 * generic Singleton<T> and specific managers classes.
 * The main purpose is reduce the duplicate codes, performance the this code organization and 
 * make easy to create new managers.
 * ------------------------------------------------------------------------------------------------
 * NOTE
 * Will be able to see comment code lines in methods and certain methos which are not neccesary yet
 * Fot this reason, when t is neccesary, can uncomment the needed method and code lines.
 * ================================================================================================
 */
public abstract class BaseScenesManager : Base {
  /**
	 * ================================================================================================
	 * Protected method
	 * 	 OnEnable
   *
	 * @Parameters: none
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Subscribe to event loaded
	 * ================================================================================================
	 */
	protected virtual void OnEnable() {
		// SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		// SceneManager.activeSceneChanged += OnActiveSceneChanged;		
	}

	/**
	 * ================================================================================================
	 * Protected methods
	 * 	 OnEnable
	 * 	 OnDestroy
   *
	 * @Parameters: none
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Unsubscribe from event loaded
	 * ================================================================================================
	 */
	protected virtual void OnDisable() {
		// SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
		// SceneManager.activeSceneChanged -= OnActiveSceneChanged;
	}
	protected virtual void OnDestroy() {
		OnDisable();
	}

  /**
	 * ================================================================================================
	 * Protected method
	 * 	 OnSceneLoaded
   *
	 * @Parameters:
	 * 		(Unity) Scene scene
	 * 		(Unity) LoadSceneMode mode
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Is called when a Scene HAS BEEN LOADED and added to hierarchy tree
	 * ================================================================================================
	 */
	protected virtual void OnSceneLoaded(Scene pSceneLoaded, LoadSceneMode pLoadMode) {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
		if(Env.Dev == Core.NODE_ENV) {
			Debug.Log($"BaseScenesManager > OnSceneLoaded::scene.name:({pSceneLoaded.name})");
			Debug.Log($"BaseScenesManager > OnSceneLoaded::mode(Load mode):({pLoadMode})");
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

		// ThingsToDoOnLoadedScene(pSceneLoaded);
	}

  /**
	 * ================================================================================================
	 * Protected method
	 * 	 OnActiveSceneChanged
   *
	 * @Parameters:
	 * 		(Unity) Scene scene
	 * 		(Unity) LoadSceneMode mode
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Is called after NEW SCENE HAS BEEN SETTED AS ACTIVE SCENE
	 * ================================================================================================
	 */
	protected virtual void OnActiveSceneChanged(Scene pOldScn, Scene pNewScn) {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
		if(Env.Dev == Core.NODE_ENV) {
			Debug.Log($"BaseScenesManager > OnActiveSceneChanged::Previous Scene({pOldScn.name})");
			Debug.Log($"BaseScenesManager > OnActiveSceneChanged::New Scene({pNewScn.name})");
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

		// ThingsToDoOnOldScene(pOldScn);
		// ThingsToDoOnNewScene(pNewScn);
	}

  /**
	 * ================================================================================================
	 * Protected method
	 * 	 OnSceneUnloaded
   *
	 * @Parameters:
	 * 		(Unity) Scene scene
	 * 		(Unity) LoadSceneMode mode
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Is called after previous Scene HAS BEEN UNLOADED
	 * ================================================================================================
	 */
	protected virtual void OnSceneUnloaded(Scene pSceneUnloaded) {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
		if(Env.Dev == Core.NODE_ENV) {
			Debug.Log($"BaseScenesManager > OnSceneUnloaded::pSceneUnloaded({pSceneUnloaded.name})");
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

		// ThingsToDoOnUnloadedScene(pSceneUnloaded);
	}

	// protected virtual void ThingsToDoOnLoadedScene(Scene pSceneLoaded){}
	// protected virtual void ThingsToDoOnOldScene(Scene pSceneLoaded){}
	// protected virtual void ThingsToDoOnNewScene(Scene pSceneLoaded){}
	// protected virtual void ThingsToDoOnUnloadedScene(Scene pSceneUnloaded){}
}