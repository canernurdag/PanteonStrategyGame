using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitAsMovable : MonoBehaviour, IUnitMovable
{
	#region DIRECT REF
	[SerializeField] private Unit _unit;
	[SerializeField] private InterfaceReference<IPlaceable> _placeable;
	[SerializeField] private UnitDataSO _unitDataSO;
	#endregion

	#region REF
	private OnUnitMoveCommand _onUnitMoveCommand;
	#endregion

	#region INTERNAL VAR
	private Tween _moveTween;
	public Transform Transform => throw new NotImplementedException();
	#endregion

	private void Start()
	{
		_onUnitMoveCommand = EventManager.Instance.GetEvent<OnUnitMoveCommand>();
		_onUnitMoveCommand.AddListener(Move);
	}

	private void OnDestroy()
	{
		_onUnitMoveCommand.RemoveListener(Move);
	}

	public void Move(Unit unit, Node targetNode)
	{
		if (unit != _unit) return;

		Node currentNode = GridManager.Instance.GetNodeFromWorldPosition(transform.position);

		var nodePath = PathfindingManager.Instance.GetPath(currentNode, targetNode);
		if (nodePath == null) return;

		Node startNode = _placeable.Value.OccupyingNodes[0];
		_placeable.Value.Deplace();

		var path = nodePath
			.Select(x=>x.transform.position)
			.ToArray();

		Move(path, _unitDataSO.MoveSpeedOnPath, () =>
		{ 
			_placeable.Value.SetOccupyingNodes(new List<Node> { targetNode });
			_placeable.Value.Place(); 
		});
	}

	public void MoveWithNodeLock(Unit unit, Node targetNode)
	{
		Node currentNode = GridManager.Instance.GetNodeFromWorldPosition(transform.position);

		var nodePath = PathfindingManager.Instance.GetPath(currentNode, targetNode);
		if (nodePath == null) return;

		Node startNode = _placeable.Value.OccupyingNodes[0];
		_placeable.Value.Deplace();

		var path = nodePath
			.Select(x => x.transform.position)
			.ToArray();

		targetNode.SetPreventPlaceableSelection(true);
		_placeable.Value.SetOccupyingNodes(new List<Node> { targetNode });
		_placeable.Value.Place();
	
		Move(path, _unitDataSO.MoveSpeedOnPath, () =>
		{
			targetNode.SetPreventPlaceableSelection(false);
		});
	}

	public void Move(Vector3 targetPosition, float speed, Action callback = null)
	{
		_moveTween?.Kill();
		_moveTween = transform.DOMove(targetPosition, speed)
			.SetSpeedBased()
			.SetEase(Ease.Linear)
			.OnComplete(() => callback?.Invoke());
	}

	public void Move(Vector3[] path, float speed, Action callback = null)
	{
		_moveTween?.Kill();
		_moveTween = transform.DOPath(path, speed, pathType: PathType.Linear, pathMode:PathMode.Sidescroller2D)
			.SetEase(Ease.Linear)
			.SetSpeedBased()
			.OnComplete(() => callback?.Invoke());
	}
}
