using UnityEngine;

/**
 * ================================================================================================
 * CameraTransitionTrigger
 * 	 Extends: MonoBehaviour
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * Handles camera behavior when the player enters or exits a trigger area.
 * It supports camera panning and swapping depending on configuration.
 * ================================================================================================
 */
public class CameraTransitionTrigger : MonoBehaviour {
	// Set index on Inspector IDE
	[SerializeField] private int targetCameraIndex = 0;

	// Serialized object containing camera behavior configurations for this trigger.
	public CamInspectorObjects cameraInspectorObjects;

	//initial camera index when transitioning
	private int _lastCameraIndex = 0;
	
	/**
	 * ================================================================================================
	 * private Method
	 * 	 OnTriggerEnter
   *
	 * @Parameters:
	 *		Collider2D other, the collider that entered the trigger
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Called when another collider enters this trigger
	 * If it's the player and camera panning is enabled, initiates camera panning
	 * ================================================================================================
	 */
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player") && cameraInspectorObjects.panCameraOnContact) {
			if (null != CameraManager.instance) {
				CameraManager.instance.PanCameraOnContact(
						cameraInspectorObjects.panDistance,
						cameraInspectorObjects.panTime,
						cameraInspectorObjects.panDirection,
						false // Not returning to start pos
				);
				CameraManager.instance.TransitionToCameraByIndex(targetCameraIndex);
				// gameObject.SetActive(false);
			} else {
				Debug.LogWarning("CameraManager.Instance es nulo!");
			}
		}else if (other.CompareTag("Player") && cameraInspectorObjects.swapCameras)
		{
			if (null != CameraManager.instance)
			{
				_lastCameraIndex = CameraManager.instance.GetCurrentCameraIndex();

				CameraManager.instance.TransitionToCameraByIndex(targetCameraIndex);
				
			}
		}
	}

	/**
	 * ================================================================================================
	 * private Method
	 * 	 OnTriggerExit
   *
	 * @Parameters:
	 *		Collider2D other, the collider that exitted the trigger
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Called when another collider enters this trigger
	 * If it's the player and camera panning is enabled, returns camera to original offset
	 * ================================================================================================
	 */
	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player") && cameraInspectorObjects.panCameraOnContact) {
			CameraManager.instance.PanCameraOnContact(
					cameraInspectorObjects.panDistance,
					cameraInspectorObjects.panTime,
					cameraInspectorObjects.panDirection,
					true // Return to start pos
			);
		}else if (other.CompareTag("Player") && cameraInspectorObjects.swapCameras)
		{
			CameraManager.instance.TransitionToCameraByIndex(_lastCameraIndex);
			//CameraManager.instance.TransitionToCameraByIndex(_lastCameraIndex);
		}
	}
}
