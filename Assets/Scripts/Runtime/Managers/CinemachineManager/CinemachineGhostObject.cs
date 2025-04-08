using DG.Tweening;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public sealed class CinemachineGhostObject : MonoBehaviour
{
	#region REF
	private OnCreatedGridDataDetermined _onCreatedGridDataDetermined;
	private OnMouseScreenPositionGive _onMouseScreenPositionGiven;
	private CreatedGridData _createdGridData;
	#endregion

	#region INTERNAL VAR
	private Tween _moveTween;
	[SerializeField] private float _edgeSize = 30f;
	[SerializeField] private float _moveAmount = 100f;
	#endregion

	private void Start()
	{

		_onCreatedGridDataDetermined = EventManager.Instance.GetEvent<OnCreatedGridDataDetermined>();
		_onMouseScreenPositionGiven = EventManager.Instance.GetEvent<OnMouseScreenPositionGive>();


		_onCreatedGridDataDetermined.AddListener(HandleCreatedGridData);
		_onMouseScreenPositionGiven.AddListener(MoveOnScreenEdges);
	}

	private void OnDestroy()
	{

		_onCreatedGridDataDetermined.RemoveListener(HandleCreatedGridData);
		_onMouseScreenPositionGiven.RemoveListener(MoveOnScreenEdges);
	}

	public void MoveOnScreenEdges(Vector3 mouseScreenPos)
	{
		if (mouseScreenPos.x > Screen.width - _edgeSize)
		{
			Vector3 targetPos = transform.position + Vector3.right*_moveAmount * Time.deltaTime;
			bool isTargetPosInsideTheBounds = IsTargetPosInBounds(targetPos);
			if(isTargetPosInsideTheBounds)
			{
				transform.position += Vector3.right * _moveAmount * Time.deltaTime;
			}
		}

		if(mouseScreenPos.x < _edgeSize)
		{
			Vector3 targetPos = transform.position + Vector3.left * _moveAmount * Time.deltaTime;
			bool isTargetPosInsideTheBounds = IsTargetPosInBounds(targetPos);
			if (isTargetPosInsideTheBounds)
			{
				transform.position += Vector3.left * _moveAmount * Time.deltaTime;
			}

		
		}

		if(mouseScreenPos.y > Screen.height - _edgeSize)
		{
			Vector3 targetPos = transform.position + Vector3.up * _moveAmount * Time.deltaTime;
			bool isTargetPosInsideTheBounds = IsTargetPosInBounds(targetPos);
			if (isTargetPosInsideTheBounds)
			{
				transform.position += Vector3.up * _moveAmount * Time.deltaTime;
			}

	
		}

		if(mouseScreenPos.y < _edgeSize)
		{
			Vector3 targetPos = transform.position + Vector3.down * _moveAmount * Time.deltaTime;
			bool isTargetPosInsideTheBounds = IsTargetPosInBounds(targetPos);
			if (isTargetPosInsideTheBounds)
			{
				transform.position += Vector3.down * _moveAmount * Time.deltaTime;
			}
		
		}

	}

	private bool IsTargetPosInBounds(Vector3 targetPos)
	{
		return targetPos.x >= _createdGridData.MinX &&
			targetPos.y >= _createdGridData.MinY &&
			targetPos.x < _createdGridData.MaxX &&
			targetPos.y < _createdGridData.MaxY;
	}

	public void HandleCreatedGridData(CreatedGridData createdGridData)
	{
		_createdGridData = createdGridData;
		MoveToPosition(_createdGridData.CenterPosition);
	}

	private void MoveToPosition(Vector3 targetPosition)
	{
		transform.position = targetPosition;
	}

	private void MoveToPosition(Vector3 targetPosition, float duration)
	{
		_moveTween?.Kill();
		_moveTween = transform.DOMove(targetPosition, duration);
	}
}
