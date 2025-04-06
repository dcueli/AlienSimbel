using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Scenes.CameraTesting.Camara
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;
        
        [Header("Controles para el YDamping de la camara cuando caes")] 
        [SerializeField] private float fallPanAmount = 0.25f;
        [SerializeField] private float fallYPanTime = 0.35f;
        public float fallSpeedYDampingChangeThreshold = -15f;
        
        public bool IsLerpingYDamping { get; private set; }
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

            for(int i = 0; i < allVirtualCameras.Length; i++)
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

        #region LERP Y DAMPING
        public void LerpYDamping(bool isPlayerFalling)
        {
            _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
        }

        private IEnumerator LerpYAction(bool isPlayerFalling)
        {
            IsLerpingYDamping = true;
            
            float startDampAmount = _framingTransposer.m_YDamping;
            float endDampAmount = 0f;

            if (isPlayerFalling)
            {
                endDampAmount = fallPanAmount;
                LerpedFromPlayerFalling = true;
            }
            else
            {
                endDampAmount = _normYPanAmount;
            }
            
            float elapsedTime = 0f;
            while (elapsedTime < fallYPanTime)
            {
                elapsedTime += Time.deltaTime;
                
                float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallYPanTime));
                _framingTransposer.m_YDamping = lerpedPanAmount;
                yield return null;
            }
            
            IsLerpingYDamping = false;
        }
        
        #endregion
        
        #region PAN CAMERA

        public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
        {
            _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
        }

        private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection,
            bool panToStartingPos)
        {
            Vector2 endPos = Vector2.zero;
            Vector2 startPos = Vector2.zero;

            if (!panToStartingPos)
            {
                switch (panDirection)
                {
                    case PanDirection.Up:
                        endPos = Vector2.up;
                        break;
                    case PanDirection.Down:
                        endPos = Vector2.down;
                        break;
                    case PanDirection.Left:
                        endPos = Vector2.left;
                        break;
                    case PanDirection.Right:
                        endPos = Vector2.right;
                        break;
                }

                endPos *= panDistance;
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
                
                Vector3 panLerp = Vector3.Lerp(startPos, endPos, (elapsedTime / panTime));
                _framingTransposer.m_TrackedObjectOffset = panLerp;
                
                yield return null;
            }
        }
        
        #endregion
    }
}