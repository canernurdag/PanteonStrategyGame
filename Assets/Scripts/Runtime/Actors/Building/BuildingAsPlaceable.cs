
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingAsPlaceable : MonoBehaviour, IPlaceable
{
	#region DIRECT REF
	[SerializeField] private Building _building;
	[field:SerializeField] public InterfaceReference<ISelectable> Selectable { get; private set; }
	[field: SerializeField] public InterfaceReference<IDamagable> Damagable { get; private set; }

	[field: SerializeField] public InterfaceReference<IAttacker> Attacker { get; private set; }
	#endregion

	#region INTERNAL VAR
	public List<Node> OccupyingNodes { get; private set; } = new();
	public bool IsPlaced { get; protected set; }
	#endregion
	#region REF
	public Transform Transform => transform;
	private OnBuildingPlaced _onBuildingPlaced;
	#endregion


	private void Start()
	{
		_onBuildingPlaced = EventManager.Instance.GetEvent<OnBuildingPlaced>();
		_onBuildingPlaced.AddListener(ExecutePlacementSequence);
	
	}

	private void OnDestroy()
	{
		_onBuildingPlaced.RemoveListener(ExecutePlacementSequence);
	}
	public void Place()
	{
		IsPlaced = true;

		OccupyingNodes.ForEach(x =>
		{
			x.SetIsOccupied(true);
			x.SetInsidePlaceable(this);
			x.ResetNodeVisual();

		});
	}


	public void ExecutePlacementSequence(Building building, Node placeNode)
	{
		if (_building != building) return;

		OccupyingNodes = GridManager.Instance.GetNodesByBuilding(placeNode, _building);
		SetOccupyingNodes(OccupyingNodes);
		Place();
	}

	public void SetOccupyingNodes(List<Node> occupyingNodes)
	{
		OccupyingNodes = occupyingNodes;
	}

	public void Deplace()
	{
		IsPlaced = true;

		OccupyingNodes.ForEach(x =>
		{
			x.SetIsOccupied(false);
			x.SetInsidePlaceable(null);
			x.ResetNodeVisual();

		}); ;
	}
}
