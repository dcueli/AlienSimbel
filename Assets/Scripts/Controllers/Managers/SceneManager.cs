using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : BaseScenesManager {
	[SerializeField] private ScenesTransition _scenesTransition;

	// ----------------------------------------------------
	// Leave this code here just in case need in the future
	// ====================================================
	/*
	protected override void ThingsToDoOnUnloadedScene(Scene pSceneUnloaded) {
		// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
		#pragma warning disable CS0162 
		if(Env.Dev == Core.NODE_ENV) {
			Debug.Log($"ScenesManager > ThingsToDoOnUnloadedScene::pSceneUnloaded.name({pSceneUnloaded.name})");
		}
		#pragma warning restore CS0162
		// END::@dcueli -> enable warning CS0162 (unrecheable code)

		// switch(pSceneUnloaded.name){
		// 	case Scenes.Main:
		// 	default:
		// 		break;
		// }
	}
	*/
	private void Awake() {
		GameManager.instance.scenesManager = this;

		if (null == _scenesTransition)
			_scenesTransition = new ScenesTransition();
	}

  public void LoadScene(string pScnName = null) {
    if (null == pScnName)
      Debug.LogError("The Scene loader has received a NULL scene name");

    if (!!!GameManager.instance.scenesManager.CheckSceneExists(pScnName))
      Debug.LogError($"The scene <pScnName:{pScnName}> not exists in the Scene loader");

    Debug.Log($"Loading scene <pScnName:{pScnName}>...");
		
		if(null != _scenesTransition)
			_scenesTransition.StartTransition(LoadInmediately, pScnName);
		else
			LoadInmediately(pScnName);
  }



	public void LoadInmediately(string pScnName) {
    SceneManager.LoadScene(pScnName);
	}

	public void LoadScnAdditive(string pScnName) {
    SceneManager.LoadScene(pScnName, LoadSceneMode.Additive);
	}

	public void UnloadScene(string pScnName) {
		//
	}

	public bool CheckSceneExists(string pScnName) {
    if (null == pScnName)
			return false;

		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			string scnPath = SceneUtility.GetScenePathByBuildIndex(i);
			string scnName = System.IO.Path.GetFileNameWithoutExtension(scnPath);

			if (scnName == pScnName)
				return true;
		}

		return false;
	}

  /**
	 * ================================================================================================
	 * Public Static method
	 * 	 GetCurrentSceneInfo
   *
	 * @Parameters: none
	 * @Returns: SceneInfo {
	 *    public string name
	 *    public string path
	 *    public int index
   * }
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Retrieve the current Scene
	 * ================================================================================================
	 */
	public SceneInfo GetCurrentSceneInfo() {
    return new SceneInfo(SceneManager.GetActiveScene());
	}
	public Scene GetCurrentSceneInfo(bool pNative) {
    return SceneManager.GetActiveScene();
	}
}