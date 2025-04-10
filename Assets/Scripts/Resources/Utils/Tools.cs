using UnityEngine;
using UnityEngine.UI;

/**
 * ================================================================================================
 * Tools
 * 	 @Extends:
 * 	 @Implements: 
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 *   A class sith several helpers function
 * ================================================================================================
 */
public static class Tools {
	/**
	* ================================================================================================
	* public static method
	* 	FindComponentInChildren<T>
  *			Where <T> is the type of object to filter and return
	*
	* @Parameters:
	*		Gameobject pParent: The parent object where search in
	*		string pGameObjectName: The object name which have to search
	*
	* @Returns: <T>
  *
	* ------------------------------------------------------------------------------------------------
	* DESCRIPTION
	* - Search only in children of an object
	* ================================================================================================
	*/
	public static T FindComponentInChildren<T>(
		GameObject pParent, 
		string pGameObjectName
	) where T : Component {

    // Search in current object
    if (pParent.name == pGameObjectName) {
			T component = pParent.GetComponent<T>();
			if (null != component)
				return component;
    }

    // Search in child recursively
    foreach (Transform child in pParent.transform) {
			T component = FindComponentInChildren<T>(child.gameObject, pGameObjectName);
			if (null != component)
				return component;
    }

    return null;

	}
}