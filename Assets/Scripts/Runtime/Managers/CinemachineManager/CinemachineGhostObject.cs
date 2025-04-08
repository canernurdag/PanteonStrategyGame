using DG.Tweening;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class CinemachineGhostObject : MonoBehaviour
{
	#region INTERNAL VAR
	private OnGridCenterPositionDetermined _onGridCenterPositionDetermined;
	private Tween _moveTween;
	#endregion

	private void Start()
	{
		_onGridCenterPositionDetermined = EventManager.Instance.GetEvent<OnGridCenterPositionDetermined>();
		_onGridCenterPositionDetermined.AddListener(MoveToPosition);
	}

	private void OnDestroy()
	{
		_onGridCenterPositionDetermined.RemoveListener(MoveToPosition);
	}

	public void MoveToPosition(Vector3 targetPosition)
	{
		transform.position = targetPosition;
	}

	private void MoveToPosition(Vector3 targetPosition, float duration)
	{
		_moveTween?.Kill();
		_moveTween = transform.DOMove(targetPosition, duration);
	}
}
