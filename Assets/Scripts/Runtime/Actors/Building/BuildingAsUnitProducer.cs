using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingAsUnitProducer : MonoBehaviour,IUnitProducer
{
	#region DIRECT REF
	[SerializeField] private List<UnitFactory> _unitFactories = new();
	[SerializeField] private Building _building;
	[SerializeField] private FlagSpawnPoint _flagSpawnPoint;
	[SerializeField] private Transform _initSpawnPos;
	[SerializeField] private BuildingAsPlaceable _buildingAsPlaceable;
	#endregion

	#region REF
	public Transform Transform => transform;
	private OnProductCreateRequest _onProductCreateRequest;
	private OnBuildingPlaced _onBuildingPlaced;
	public Node StartSpawnNode { get; private set; }
	public Node FlagSpawnNode { get; private set; }
	#endregion

	private void Start()
	{
		_onProductCreateRequest = EventManager.Instance.GetEvent<OnProductCreateRequest>();
		_onBuildingPlaced = EventManager.Instance.GetEvent<OnBuildingPlaced>();

		_onProductCreateRequest.AddListener(ExecuteProduceUnitSequnece);
		_onBuildingPlaced.AddListener(HandleStartSpawnNode);
	}
	private void OnDestroy()
	{
		_onProductCreateRequest.RemoveListener(ExecuteProduceUnitSequnece);
		_onBuildingPlaced.RemoveListener(HandleStartSpawnNode);
	}

	public void ExecuteProduceUnitSequnece(Building building, UnitDataSO unitDataSO)
	{
		if (building != _building) return;

		var spawnNode = GetNextEmptyNeighbourNode();
		if (spawnNode == null)
		{
			//WARN USER
			return;
		}

		var unit = ProduceUnit(unitDataSO.UnitType);
		PlaceUnitToInitPosition(unit);

		var firstSpawnNode = spawnNode;
		List<Node> occupyingNodes = new(){firstSpawnNode};

		MoveProducedUnitToFirstNode(unit, firstSpawnNode, () => 
		{
			var nearestNode = PathfindingManager.Instance.FindNearestUnoccupiedAndNoPreventNode(_flagSpawnPoint.OccupyingNodes[0]);
			var unitAsMovable = unit.UnitAsMovable.Value as UnitAsMovable;
			unitAsMovable.MoveWithNodeLock(unit, nearestNode);
		});

		var placeable = unit.UnitAsPlaceable.Value;
		placeable.SetOccupyingNodes(occupyingNodes);
		placeable.Place();
	}

	public Unit ProduceUnit(UnitType unitType)
	{
		var factory = GetFactory(unitType);
		if(factory == null)
		{
			//WARN USER

			return null;
		}

		var unit = factory.CreateUnit();
		return unit;

	}

	public void MoveProducedUnitToFirstNode(Unit unit, Node firstNode, Action callback)
	{
		unit.UnitAsMovable.Value.Move(firstNode.transform.position, 0.5f, callback); 
	}

	private void PlaceUnitToInitPosition(Unit unit)
	{
		unit.transform.position = _initSpawnPos.position;
	}

	private UnitFactory GetFactory(UnitType unitType)
	{
		return _unitFactories.FirstOrDefault(x=>x.UnitType == unitType);
	}



	public Node GetNextEmptyNeighbourNode()
	{
		if (!StartSpawnNode.IsOccupied) return StartSpawnNode;

		var nextEmptyNegihbourNode = GetNextUnoccupiedNeighbourNode();
		if (nextEmptyNegihbourNode) return nextEmptyNegihbourNode;

		return null;
	}

	public Node GetNextUnoccupiedNeighbourNode()
	{
		var orderedOccupyingNodes = _buildingAsPlaceable.OccupyingNodes
										.OrderBy(x=>Vector3.Distance(StartSpawnNode.transform.position, x.transform.position))
										.ToList();

		for (int i = 0; i < orderedOccupyingNodes.Count; i++)
		{
			var buildingNode = orderedOccupyingNodes[i];

			var neighbourNodes = GridManager.Instance.GetNeighbours(buildingNode);
			var unoccupiedNode = neighbourNodes.FirstOrDefault(x => !x.IsOccupied);

			if (unoccupiedNode != null) return unoccupiedNode;

		}
		return null;
	}

	public void HandleStartSpawnNode(Building building, Node placeNode)
	{
		if (_building != building) return;

		StartSpawnNode = GridManager.Instance.GetNodeByOffset(placeNode, _building.BuildingDataSO.PreferedSpawnNodeHorizontal, _building.BuildingDataSO.PreferedSpawnNodeVertical);
		_flagSpawnPoint.Place(StartSpawnNode);
	}
}
