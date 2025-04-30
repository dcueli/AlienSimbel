using System;
using System.Collections;
using Cinemachine;
using UnityEngine;


/**
 * ================================================================================================
 * CameraManager
 *
 * Extends: Singleton<T>
 * Implements: None
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * Manages camera behavior, including Y-axis damping changes on player fall
 * and camera panning when the player interacts with triggers.
 * ================================================================================================
 */
public class CameraManager : BaseCameraManager {

	private Coroutine _lerpYPanCoroutine;
	private Coroutine _panCameraCoroutine;

	private CinemachineVirtualCamera _currentCamera;
	private CinemachineFramingTransposer _framingTransposer;

	private float _normYPanAmount;
	private Vector2 _startingTrackedObjectOffset;

	// New field to camera management
	// It could use a lower priority for all disabled cameras
	[SerializeField]
	private int baseCameraPriority = 1;
	// And a higher priprity for the cameras to enable
	[SerializeField]
	private int activeCameraPriority = 100;

	protected override void Awake() {
		// It could improve this loop to find out the initial camera and ensure the proper 
		// priority of the left of cameras
		for (int i = 0; i < allVirtualCameras.Length; i++) {
			// To avoid errors if there ara void spaces
			if (null == allVirtualCameras[i]) continue;

			// Config all cameras with base priority initially
			allVirtualCameras[i].m_Priority = baseCameraPriority;

			// Find the initial camera if it is enabled (or it has the configured enabled priotiry)
			// If the a enabled camera at the init must be the current camera
			if (allVirtualCameras[i].enabled || allVirtualCameras[i].m_Priority == activeCameraPriority) {
				_currentCamera = allVirtualCameras[i];
				// To ensure the initial camera has the enabled priority if the CinemachineBrain select it
				_currentCamera.m_Priority = activeCameraPriority;
			}
		}

		// If it doesn't find any initial camera or the scene begins without any 
		// enabled virtual camera with higher priority
		if (null != _currentCamera) {
			_framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

			// To ensure  these values ar initializing from the current camera
			if (_framingTransposer != null) {
				_normYPanAmount = _framingTransposer.m_YDamping;
				_startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
			} else {
				Debug.LogWarning("La cámara virtual inicial '" + _currentCamera.name + "' no tiene un Framing Transposer.");
			}
		} else {
			Debug.LogWarning("Ninguna Cinemachine Virtual Camera inicial encontrada o configurada para iniciar activa. ¡Las transiciones pueden no funcionar hasta que actives una cámara!");
		}

	}

	// Starts interpolating the Y damping value based on player falling state.
	public void LerpYDamping(bool isPlayerFalling) {
		_lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
	}

	private IEnumerator LerpYAction(bool isPlayerFalling) {
		IsLerpingYDamping = true;

		float startDampAmount = _framingTransposer.m_YDamping;
		float endDampAmount = isPlayerFalling ? fallPanAmount : _normYPanAmount;
		if (isPlayerFalling) LerpedFromPlayerFalling = true;

		float elapsedTime = 0f;
		while (elapsedTime < fallYPanTime)
		{
			elapsedTime += Time.deltaTime;
			float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime / fallYPanTime);
			_framingTransposer.m_YDamping = lerpedPanAmount;
			yield return null;
		}

		IsLerpingYDamping = false;
	}

	/// <summary>
	/// Initiates a camera pan movement when triggered by a player interaction.
	/// </summary>
	/// <param name="panDistance">Distance to pan.</param>
	/// <param name="panTime">Duration of the pan animation.</param>
	/// <param name="panDirection">Direction of the pan.</param>
	/// <param name="panToStartingPos">Whether to return to the starting offset.</param>
	public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
	{
		_panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
	}

	private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
	{
		Vector2 endPos = Vector2.zero;
		Vector2 startPos = Vector2.zero;

		if (!panToStartingPos)
		{
			endPos = panDirection switch
			{
				PanDirection.Up => Vector2.up,
				PanDirection.Down => Vector2.down,
				PanDirection.Left => Vector2.left,
				PanDirection.Right => Vector2.right,
				_ => Vector2.zero
			} * panDistance;

			startPos = _startingTrackedObjectOffset;
			endPos += _startingTrackedObjectOffset;
		}
		else
		{
			startPos = _framingTransposer.m_TrackedObjectOffset;
			endPos = _startingTrackedObjectOffset;
		}

		float elapsedTime = 0f;
		while (elapsedTime < panTime)
		{
			elapsedTime += Time.deltaTime;
			_framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(startPos, endPos, elapsedTime / panTime);
			yield return null;
		}
	}
}