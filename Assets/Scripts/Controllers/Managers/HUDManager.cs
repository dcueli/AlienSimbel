using UnityEngine;
using TMPro;

/**
 * ================================================================================================
 * HUDManager
 *
 * Extends: Base
 * Implements: None
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * ================================================================================================
 */
public class HUDManager : Base {
	[SerializeField] private TextMeshProUGUI serializedPropName;

	protected override void Start() {
		// 
	}

	protected override void Update() {
		if (null == GameManager.instance)
			return;

		// 
	}
}
