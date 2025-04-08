using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T Instance;

	// (Optional) This indicates whether persist between scenes
	// [SerializeField] private bool IsPersistent = false;

	public static T instance {
		get {
			if (null == Instance) {
				Instance = FindObjectOfType<T>();

				if (null == Instance) {
					GameObject obj = new GameObject();
					Instance = obj.AddComponent<T>();
					obj.name = typeof(T).ToString();
				}
			}

			return Instance;
		}
	}


  /**
	 * ================================================================================================
	 * Protected Virtual Method
	 * 	 Awake
   *
	 * @Parameters:
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * The "Virtual" key word means this method may be override on children classes. It means let 
	 * children classes implement their Awake methods if need to do additional actions further 
	 * logic of Songleton pattern.
	 * ================================================================================================
	 */
	protected virtual void Awake() {
		if (null == Instance) {
			Instance = this as T;
			// if (IsPersistent)			
				DontDestroyOnLoad(gameObject); 
		} else if (this != Instance) {
			// BEGIN::@dcueli -> disable warning CS0162 (unrecheable code)
			#pragma warning disable CS0162
			if(Env.Dev == Core.NODE_ENV)
				Debug.LogWarning($"There is another instance of {typeof(T).Name}, destroying {gameObject.name}...");
			#pragma warning restore CS0162
			// END::@dcueli -> enable warning CS0162 (unrecheable code)

			Destroy(gameObject);
		}
	}
}