using Cinemachine;
using UnityEngine;

/**
 * ================================================================================================
 * CameraTransitionTrigger
 * 	 Extends: 	 CameraTransitionTrigger
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * Serializable class to hold camera behavior configuration for use in triggers and custom
 * editor tools.
 * ================================================================================================
 */
[System.Serializable]
public class CamInspectorObjects {
	// If enabled, cameras will be swapped when the trigger is activated.
	public bool swapCameras = false;

	// If enabled, the camera will pan in a direction when the trigger is activated.
	public bool panCameraOnContact = false;

	// Camera to activate when the player moves left (used in swap logic).
	[HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
	
	// Camera to activate when the player moves right (used in swap logic).
	[HideInInspector] public CinemachineVirtualCamera cameraOnRight;
	
	// Direction the camera should pan when panning is enabled.
	[HideInInspector] public EPanDirection panDirection;
	
	// Distance to pan the camera.
	[HideInInspector] public float panDistance = 3f;
	
	// Time in seconds for the pan animation to complete.
	[HideInInspector] public float panTime = 0.35f;
}
