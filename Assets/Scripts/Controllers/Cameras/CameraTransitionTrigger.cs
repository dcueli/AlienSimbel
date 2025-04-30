using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Handles camera behavior when the player enters or exits a trigger area.
/// It supports camera panning and swapping depending on configuration.
/// </summary>
public class CameraTransitionTrigger : MonoBehaviour {
	/// <summary>
	/// Serialized object containing camera behavior configurations for this trigger.
	/// </summary>
	public CustomInspectorObjects customInspectorObjects;

	private Collider2D _collider2D;

	private void Start()
	{
		_collider2D = GetComponent<Collider2D>();
	}

	/// <summary>
	/// Called when another collider enters this trigger.
	/// If it's the player and camera panning is enabled, initiates camera panning.
	/// </summary>
	/// <param name="other">The collider that entered the trigger.</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && customInspectorObjects.panCameraOnContact)
		{
			CameraManager.Instance.PanCameraOnContact(
					customInspectorObjects.panDistance,
					customInspectorObjects.panTime,
					customInspectorObjects.panDirection,
					false // Not returning to start pos
			);
		}
	}

	/// <summary>
	/// Called when another collider exits this trigger.
	/// If it's the player and camera panning is enabled, returns camera to original offset.
	/// </summary>
	/// <param name="other">The collider that exited the trigger.</param>
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") && customInspectorObjects.panCameraOnContact)
		{
			CameraManager.Instance.PanCameraOnContact(
					customInspectorObjects.panDistance,
					customInspectorObjects.panTime,
					customInspectorObjects.panDirection,
					true // Return to start pos
			);
		}
	}
}

/// <summary>
/// Serializable class to hold camera behavior configuration
/// for use in triggers and custom editor tools.
/// </summary>
[System.Serializable]
public class CustomInspectorObjects
{
	/// <summary>
	/// If enabled, cameras will be swapped when the trigger is activated.
	/// </summary>
	public bool swapCameras = false;

	/// <summary>
	/// If enabled, the camera will pan in a direction when the trigger is activated.
	/// </summary>
	public bool panCameraOnContact = false;

	/// <summary>
	/// Camera to activate when the player moves left (used in swap logic).
	/// </summary>
	[HideInInspector] public CinemachineVirtualCamera cameraOnLeft;

	/// <summary>
	/// Camera to activate when the player moves right (used in swap logic).
	/// </summary>
	[HideInInspector] public CinemachineVirtualCamera cameraOnRight;

	/// <summary>
	/// Direction the camera should pan when panning is enabled.
	/// </summary>
	[HideInInspector] public PanDirection panDirection;

	/// <summary>
	/// Distance to pan the camera.
	/// </summary>
	[HideInInspector] public float panDistance = 3f;

	/// <summary>
	/// Time in seconds for the pan animation to complete.
	/// </summary>
	[HideInInspector] public float panTime = 0.35f;
}

/// <summary>
/// Enum defining the possible directions the camera can pan when triggered.
/// </summary>
public enum PanDirection
{
	Up,
	Down,
	Left,
	Right
}

/// <summary>
/// Custom inspector for <see cref="CameraTransitionTrigger"/>.
/// Dynamically shows or hides fields depending on boolean flags set in <see cref="CustomInspectorObjects"/>.
/// </summary>
[CustomEditor(typeof(CameraTransitionTrigger))]
public class MyScriptEditor : Editor
{
	private CameraTransitionTrigger cameraControlTrigger;

	private void OnEnable()
	{
		cameraControlTrigger = (CameraTransitionTrigger)target;
	}

	/// <summary>
	/// Draws the custom inspector UI in the Unity Editor.
	/// </summary>
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (cameraControlTrigger.customInspectorObjects.swapCameras)
		{
			cameraControlTrigger.customInspectorObjects.cameraOnLeft =
					EditorGUILayout.ObjectField("Camera on left",
							cameraControlTrigger.customInspectorObjects.cameraOnLeft,
							typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

			cameraControlTrigger.customInspectorObjects.cameraOnRight =
					EditorGUILayout.ObjectField("Camera on right",
							cameraControlTrigger.customInspectorObjects.cameraOnRight,
							typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
		}

		if (cameraControlTrigger.customInspectorObjects.panCameraOnContact)
		{
			cameraControlTrigger.customInspectorObjects.panDirection =
					(PanDirection)EditorGUILayout.EnumPopup("Camera Pan direction",
							cameraControlTrigger.customInspectorObjects.panDirection);

			cameraControlTrigger.customInspectorObjects.panDistance =
					EditorGUILayout.FloatField("Pan distance",
							cameraControlTrigger.customInspectorObjects.panDistance);

			cameraControlTrigger.customInspectorObjects.panTime =
					EditorGUILayout.FloatField("Pan Time",
							cameraControlTrigger.customInspectorObjects.panTime);
		}

		if (GUI.changed)
		{
			EditorUtility.SetDirty(cameraControlTrigger);
		}
	}
}
