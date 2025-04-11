using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingAsUnitProducer : MonoBehaviour,IUnitProducer
{
	#region DIRECT REF
	[SerializeField] private List<UnitFactory> _unitFactories = new();
	[SerializeField] private Building _building;
	[SerializeField] private BuildingSpawnPoint _buildingSpawnPoint;
	[SerializeField] private Transform _initSpawnPos;
	[SerializeField] private BuildingAsPlaceable _buildingAsPlaceable;
	#endregion

	#region REF
	public Transform Transform => transform;
	private OnProductCreateRequest _onProductCreateRequest;
	private OnBuildingPlaced _onBuildingPlaced;
	public Node PreferedSpawnNode { get; private set; }
	#endregion

	private void Start()
	{
		_onProductCreateRequest = EventManager.Instance.GetEvent<OnProductCreateRequest>();
		_onBuildingPlaced = EventManager.Instance.GetEvent<OnBuildingPlaced>();

		_onProductCreateRequest.AddListener(ExecuteProduceUnitSequnece);
		_onBuildingPlaced.AddListener(SetPreferedSpawnedNode);
	}
	private void OnDestroy()
	{
		_onProductCreateRequest.RemoveListener(ExecuteProduceUnitSequnece);
		_onBuildingPlaced.RemoveListener(SetPreferedSpawnedNode);
	}

	public void ExecuteProduceUnitSequnece(Building building, UnitDataSO unitDataSO)
	{
		if (building != _building) return;

		var spawnNode = GetSpawnNode();
		if (spawnNode == null)
		{
			//WARN USER
			return;
		}

		var unit = ProduceUnit(unitDataSO.UnitType);
		PlaceUnitToInitPosition(unit);

		var firstSpawnNode = PreferedSpawnNode;
		List<Node> occupyingNodes = new(){firstSpawnNode};

		MoveProducedUnitToFirstNode(unit, firstSpawnNode);

		var placeable = unit.UnitAsPlaceable.Value;
		placeable.SetOccupyingNodes(occupyingNodes);
		placeable.SetAsPlaced();
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

	public void MoveProducedUnitToFirstNode(Unit unit, Node firstNode)
	{
		unit.UnitAsMovable.Value.Move(firstNode.transform.position, 0.5f); 
	}

	private void PlaceUnitToInitPosition(Unit unit)
	{
		unit.transform.position = _initSpawnPos.position;
	}

	private UnitFactory GetFactory(UnitType unitType)
	{
		return _unitFactories.FirstOrDefault(x=>x.UnitType == unitType);
	}



	public Node GetSpawnNode()
	{
		bool isPrefredSpawnNodeOccupied = PreferedSpawnNode.IsOccupied;
		if (!isPrefredSpawnNodeOccupied) return PreferedSpawnNode;

		var nextEmptyNegihbourNode = GetNextUnoccupiedNeighbourNode();
		if (nextEmptyNegihbourNode) return nextEmptyNegihbourNode;

		return null;
	}

	public Node GetNextUnoccupiedNeighbourNode()
	{
		for (int i = 0; i < _buildingAsPlaceable.OccupyingNodes.Count; i++)
		{
			var buildingNode = _buildingAsPlaceable.OccupyingNodes[i];

			var neighbourNodes = GridManager.Instance.GetNeighbours(buildingNode);
			var unoccupiedNode = neighbourNodes.FirstOrDefault(x => !x.IsOccupied);

			if (unoccupiedNode != null) return unoccupiedNode;

		}
		return null;
	}

	public void SetPreferedSpawnedNode(Building building, Node placeNode)
	{
		if (_building != building) return;

		PreferedSpawnNode = GridManager.Instance.GetNodeByOffset(placeNode, _building.BuildingDataSO.PreferedSpawnNodeHorizontal, _building.BuildingDataSO.PreferedSpawnNodeVertical);
	}
}
