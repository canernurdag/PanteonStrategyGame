using DG.Tweening;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public sealed class CinemachineGhostObject : MonoBehaviour
{
	#region REF
	private OnCreatedGridDataDetermined _onCreatedGridDataDetermined;
	private OnMouseScreenPositionGiven _onMouseScreenPositionGiven;
	private CreatedGridData _createdGridData;
	private Camera _mainCamera;
	#endregion

	#region INTERNAL VAR
	private Tween _moveTween;
	[SerializeField] private float _edgeSize = 30f;
	[SerializeField] private float _moveAmount = 100f;
	private float _cameraRenderHorizontalDistanceHalf, _cameraRenderVerticalDistanceHalf;
	#endregion

	private void Awake()
	{
		_mainCamera = Camera.main;
	}


	private void Start()
	{
		_onCreatedGridDataDetermined = EventManager.Instance.GetEvent<OnCreatedGridDataDetermined>();
		_onMouseScreenPositionGiven = EventManager.Instance.GetEvent<OnMouseScreenPositionGiven>();


		_onCreatedGridDataDetermined.AddListener(HandleCreatedGridData);
		_onMouseScreenPositionGiven.AddListener(MoveOnScreenEdges);

		SetCameraRenderDistancesInWorldSpace();
	}

	private void OnDestroy()
	{

		_onCreatedGridDataDetermined.RemoveListener(HandleCreatedGridData);
		_onMouseScreenPositionGiven.RemoveListener(MoveOnScreenEdges);
	}

	private void SetCameraRenderDistancesInWorldSpace()
	{
		_cameraRenderVerticalDistanceHalf = _mainCamera.orthographicSize;
		float aspectRatio = (float)Screen.width / (float)Screen.height;
		_cameraRenderHorizontalDistanceHalf = _cameraRenderVerticalDistanceHalf * aspectRatio;
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
		return targetPos.x >= _createdGridData.MinX + _cameraRenderHorizontalDistanceHalf &&
			targetPos.y >= _createdGridData.MinY + _cameraRenderVerticalDistanceHalf &&
			targetPos.x < _createdGridData.MaxX - _cameraRenderHorizontalDistanceHalf+1 &&
			targetPos.y < _createdGridData.MaxY - _cameraRenderVerticalDistanceHalf+1;
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
