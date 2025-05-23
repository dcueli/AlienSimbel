using Cinemachine;
using UnityEngine;

/**
 * ================================================================================================
 * BaseCameraManager
 * 	 Extends: 	 Singleton<CameraManager>
 *   Implements: IGameManager, IButtonsManager
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * As a Abstract base Class that provide ths persistent Singleton pattern functionality a common 
 * base of all of kind managers of the Videogame and the same time, it is a intermediary between 
 * generic Singleton<T> and specific managers classes.
 * The main purpose is reduce the duplicate codes, performance the this code organization and 
 * make easy to create new managers.
 * ================================================================================================
 */
public abstract class BaseCameraManager : Singleton<CameraManager> {
  // To know what enviroment it is
  [SerializeField] public GameObject DebugMsgObj;

	[SerializeField]
	protected CinemachineVirtualCamera[] allVirtualCameras;

	[Header("Controls for the camera YDamping when the player falls")]
	[SerializeField]
	protected float fallPanAmount = 0.25f;

	[SerializeField]
	protected float fallYPanTime = 0.35f;

	// Threshold fall speed for triggering Y damping change.
	public float fallSpeedYDampingChangeThreshold = -15f;

	// Indicates whether the camera is currently interpolating the Y damping.
	public bool isLerpingYDamping { get; set; }

	// Indicates whether the Y damping was changed due to player falling.
	public bool isLerpedFromPlayerFalling { get; set; }
}