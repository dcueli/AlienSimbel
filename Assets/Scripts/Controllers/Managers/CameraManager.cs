using System;
using System.Collections;
using Cinemachine;
using UnityEngine;


/**
 * ================================================================================================
 * Public method
 * 		CameraManager
 *
 * Extends: BaseCameraManager
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
	[SerializeField] private int baseCameraPriority = 1;
	// And a higher priprity for the cameras to enable
	[SerializeField] private int activeCameraPriority = 100;

	protected override void Awake() {
		if (null != allVirtualCameras) {
			// It could improve this loop to find out the initial camera and ensure the proper 
			// priority of the left of cameras
			for (int i = 0; i < allVirtualCameras.Length; i++) {
				// To avoid errors if there ara void spaces
				if (null == allVirtualCameras[i]) continue;

				// Config all cameras with base priority initially
				allVirtualCameras[i].m_Priority = baseCameraPriority;

				// Find the initial camera if it is enabled (or it has the configured enabled priotiry)
				// If the a enabled camera at the init must be the current camera
				if (allVirtualCameras[i].m_Priority == activeCameraPriority || allVirtualCameras[i].gameObject.activeSelf) {
					_currentCamera = allVirtualCameras[i];
					// To ensure the initial camera has the enabled priority if the CinemachineBrain select it
					_currentCamera.m_Priority = activeCameraPriority;
				}
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
				Debug.LogWarning("The initial virtual camera '" + _currentCamera.name + "' has not a Framing Transposer.");
			}
		} else {
			Debug.LogWarning("No enable initial virtual camera founded or setted up to start. Transition can not work to enable a camera!");
		}
	}

	/**
	 * ================================================================================================
	 * Public Method
	 * 	 TransitionToCameraByIndex
   *
	 * @Parameters:
	 *		int cameraIndex, Index of the caemra in Array <allVirtualCameras>
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Transitions to the proper Cinemachine virtual camera by its index in <allVirtualCameras>
	 * The CinemachineBrain manages the smooth transition automatically and it'll detect 
	 * priority changes in the Main camera, and it makes the transition using the Blend configuration
	 * ================================================================================================
	 */
	public void TransitionToCameraByIndex(int pCameraIndex) {
		// Validate camera index
		if (0 > pCameraIndex || pCameraIndex >= allVirtualCameras.Length) {
			Debug.LogWarning("Índice de cámara fuera de rango: " + pCameraIndex);
			return;
		}

		CinemachineVirtualCamera targetCamera = allVirtualCameras[pCameraIndex];

		// Validate if camera is not NULL and is not current enable camera
		if (null == targetCamera) {
			Debug.LogWarning("La Virtual Camera en el índice " + pCameraIndex + " es nula.");
			return;
		}
		// If already using this camera
		if (targetCamera == _currentCamera)
		{
			return;
		}

		// Lower priority of the current camera if it exists and is different
		// Is important to check if the same camera in case it is called several 
		// times with the same index
		if (null != _currentCamera)
			_currentCamera.m_Priority = baseCameraPriority;

		// Higher priority of the new target camera
		targetCamera.m_Priority = activeCameraPriority;

		// Update the reference to the current camera and its Framing Transposer to 
		// the function <damping, pan> works on it
		_currentCamera = targetCamera;
		_framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
		_currentCamera.gameObject.SetActive(true);
		// If the transposer exists, also update reference offsets for the panning/damping
		if (null != _framingTransposer) {
			_startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
			// Although it could be calculated, reset if it already lerped
			_normYPanAmount = _framingTransposer.m_YDamping;
		} else {
			// Decide wheter disable <iIsLerpingYDamping> and PanCamera Coroutines if 
			// there is no transposer
			Debug.LogWarning("La nueva cámara activa '" + _currentCamera.name + "' no tiene un Framing Transposer.");
		}
	}	

	/**
	 * ================================================================================================
	 * Public Method
	 * 	 LerpYDamping
   *
	 * @Parameters:
	 *		bool pIsPlayerFalling
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Starts interpolating the Y damping value based on player falling state
	 * ================================================================================================
	 */
	public void LerpYDamping(bool pIsPlayerFalling) {
		// Make sure if have a current camera with a transposer before try lerp
		if (_framingTransposer == null) return;

		_lerpYPanCoroutine = StartCoroutine(LerpYAction(pIsPlayerFalling));
	}

	/**
	 * ================================================================================================
	 * private Method
	 * 	 LerpYAction
   *
	 * @Parameters:
	 *		bool pIsPlayerFalling
	 * @Returns: IEnumerator
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Since this method is from initial class, honestly, I dont know what it does this. 
	 * When I find out, I will comment this description
	 * ================================================================================================
	 */
	private IEnumerator LerpYAction(bool pIsPlayerFalling) {
		isLerpingYDamping = true;

		// Make sure to get the current damping start of the transposer
		float startDampAmount = _framingTransposer.m_YDamping;
		// <_normYPanAmount> is updated on camera change if the transposer exists
		float endDampAmount = pIsPlayerFalling ? fallPanAmount : _normYPanAmount;

		if (pIsPlayerFalling) 
			isLerpedFromPlayerFalling = true;

		float elapsedTime = 0f;
		while (elapsedTime < fallYPanTime) {
			elapsedTime += Time.deltaTime;
			float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime / fallYPanTime);
			_framingTransposer.m_YDamping = lerpedPanAmount;
			yield return null;
		}

		isLerpingYDamping = false;
		// Make sure the <_lerpYPanCoroutine> is NULL when it is finished only if is needed
		_lerpYPanCoroutine = null;
	}

	/**
	 * ================================================================================================
	 * public Method
	 * 	 PanCameraOnContact
   *
	 * @Parameters:
	 *		bool pPanDistance, distance to pan
	 *		bool pPanTime, duration of the pan animation
	 *		bool pPanDirection, direction of the pan
	 *		bool pPanToStartingPos, whether to return to the starting offset
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Start a camera panoramic movement when triggered by a player interaction
	 * ================================================================================================
	 */
	public void PanCameraOnContact(
		float pPanDistance, 
		float pPanTime, 
		EPanDirection pPanDirection, 
		bool pPanToStartingPos
	) {
		// Make sure if a current camera exists with a transposer before try panoramic
		if (_framingTransposer == null)
			return;
		// Stop Coroutine if was already running to avoid conflicts
		if (_panCameraCoroutine != null)
			StopCoroutine(_panCameraCoroutine);

		_panCameraCoroutine = StartCoroutine(PanCamera(pPanDistance, pPanTime, pPanDirection, pPanToStartingPos));
	}

	private IEnumerator PanCamera(
		float pPanDistance, 
		float pPanTime, 
		EPanDirection pPanDirection, 
		bool pPanToStartingPos
	) {
		Vector2 endPos = Vector2.zero;
		Vector2 startPos = Vector2.zero;

		if (!!!pPanToStartingPos) {
			endPos = pPanDirection switch {
				EPanDirection.Up 		=> Vector2.up,
				EPanDirection.Down 	=> Vector2.down,
				EPanDirection.Left 	=> Vector2.left,
				EPanDirection.Right 	=> Vector2.right,
				_ 									=> Vector2.zero
			} * pPanDistance;

			// Use the initial offset reference of the current camera
			startPos = _startingTrackedObjectOffset;
			endPos += _startingTrackedObjectOffset;
		} else {
			// Use the current position of the pan like init
			startPos = _framingTransposer.m_TrackedObjectOffset;
			// The target is back to the initial offset reference of the current camera
			endPos = _startingTrackedObjectOffset;
		}

		float elapsedTime = 0f;
		while (elapsedTime < pPanTime) {
			elapsedTime += Time.deltaTime;
			// Make sure to use the enabled camera transposer during the pan
			// If another camera is enabled during the pan (by another transition), this Coroutine 
			// will continue working on the camera transposer where the pan was started
			_framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(startPos, endPos, elapsedTime / pPanTime);
			yield return null;
		}

		// Asegúrate de que _panCameraCoroutine sea null cuando termina si es necesario
		_panCameraCoroutine = null;		
	}

	/**
	 * ================================================================================================
	 * public Method
	 * 	 GetCurrentCameraIndex
   *
	 * @Parameters: none
	 * @Returns: int, the index of the current active virtual camera in <allVirtualCameras>
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Searches through the array <allVirtualCameras> to find the one that matches the current
	 * active camera <_currentCamera>. Returns its index if found, or -1 if the current camera
	 * is not in the array.
	 * ================================================================================================
	 */
	public int GetCurrentCameraIndex() {
		for (int i = 0; i < allVirtualCameras.Length; i++) {
			if (allVirtualCameras[i] == _currentCamera)
				return i;
		}
		return -1;
	}
	
	
}