using UnityEngine;

/**
 * ================================================================================================
 * CameraHorizontalPosition
 * 	 Extends: MonoBehaviour
 * ------------------------------------------------------------------------------------------------
 * DESCRIPTION
 * Adjusts the camera target's horizontal position based on player input direction.
 * ================================================================================================
 */
public class CameraHorizontalPosition : MonoBehaviour {
	[SerializeField] private GameObject _cameraTarget;

	[SerializeField] private PlayerInput _playerInput;

	[SerializeField] private float expectedHorPos = 1f;

	[SerializeField] private float lerpSpeed = 5f;

	private float _desiredX = 0f;

	/**
	 * ================================================================================================
	 * private Method
	 * 	 FixedUpdate
   *
	 * @Returns: void
	 * ------------------------------------------------------------------------------------------------
	 * DESCRIPTION
	 * Updates the camera target's horizontal position every fixed frame, creating a 
	 * directional horizontal position
	 * ================================================================================================
	 */
	private void FixedUpdate() {
		float inputX = _playerInput.Movement.x;

		_desiredX = inputX > 0.1f ? expectedHorPos : -expectedHorPos;

		Vector3 localTargetPos = _cameraTarget.transform.localPosition;
		localTargetPos.x = Mathf.Lerp(localTargetPos.x, _desiredX, Time.deltaTime * lerpSpeed);
		_cameraTarget.transform.localPosition = localTargetPos;
	}
}
