using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Scenes.CameraTesting.Camara
{
    /// <summary>
/// Manages camera behavior, including Y-axis damping changes on player fall
/// and camera panning when the player interacts with triggers.
/// </summary>
public class CameraManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the <see cref="CameraManager"/>.
    /// </summary>
    public static CameraManager Instance { get; private set; }

    [SerializeField]
    private CinemachineVirtualCamera[] allVirtualCameras;

    [Header("Controles para el YDamping de la cámara cuando caes")]
    [SerializeField]
    private float fallPanAmount = 0.25f;

    [SerializeField]
    private float fallYPanTime = 0.35f;

    /// <summary>
    /// Threshold fall speed for triggering Y damping change.
    /// </summary>
    public float fallSpeedYDampingChangeThreshold = -15f;

    /// <summary>
    /// Indicates whether the camera is currently interpolating the Y damping.
    /// </summary>
    public bool IsLerpingYDamping { get; private set; }

    /// <summary>
    /// Indicates whether the Y damping was changed due to player falling.
    /// </summary>
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYPanAmount;
    private Vector2 _startingTrackedObjectOffset;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        for (int i = 0; i < allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].enabled)
            {
                _currentCamera = allVirtualCameras[i];
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        _normYPanAmount = _framingTransposer.m_YDamping;
        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
    }

    /// <summary>
    /// Starts interpolating the Y damping value based on player falling state.
    /// </summary>
    /// <param name="isPlayerFalling">Whether the player is falling.</param>
    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
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

}