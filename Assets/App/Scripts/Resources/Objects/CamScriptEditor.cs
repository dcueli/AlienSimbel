using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

/**
 * ================================================================================================
 * MyScriptEditor
 * 	 Extends: 	 CameraTransitionTrigger
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * Custom inspector for <see cref="CameraTransitionTrigger"/>
 * Dynamically shows or hides fields depending on boolean flags set in 
 * <see cref="CamInspectorObjects"/>
 * ================================================================================================
 */

[CustomEditor(typeof(CameraTransitionTrigger))]
public class CamScriptEditor : Editor {
	private CameraTransitionTrigger _CamTransTrigger;

	private void OnEnable() {
		_CamTransTrigger = (CameraTransitionTrigger)target;
	}

	/// Draws the custom inspector UI in the Unity Editor.
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if (_CamTransTrigger.cameraInspectorObjects.swapCameras) {
			_CamTransTrigger.cameraInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField(
				"Camera on left",
				_CamTransTrigger.cameraInspectorObjects.cameraOnLeft,
				typeof(CinemachineVirtualCamera), 
				true
			) as CinemachineVirtualCamera;

			_CamTransTrigger.cameraInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField(
				"Camera on right",
				_CamTransTrigger.cameraInspectorObjects.cameraOnRight,
				typeof(CinemachineVirtualCamera), 
				true
			) as CinemachineVirtualCamera;
		}

		if (_CamTransTrigger.cameraInspectorObjects.panCameraOnContact) {
			_CamTransTrigger.cameraInspectorObjects.panDirection = (EPanDirection)EditorGUILayout.EnumPopup(
				"Camera Pan direction",
				_CamTransTrigger.cameraInspectorObjects.panDirection
			);

			_CamTransTrigger.cameraInspectorObjects.panDistance = EditorGUILayout.FloatField(
				"Pan distance",
				_CamTransTrigger.cameraInspectorObjects.panDistance
			);

			_CamTransTrigger.cameraInspectorObjects.panTime = EditorGUILayout.FloatField(
				"Pan Time",
				_CamTransTrigger.cameraInspectorObjects.panTime
			);
		}

		if (GUI.changed) {
			EditorUtility.SetDirty(_CamTransTrigger);
		}
	}
}
