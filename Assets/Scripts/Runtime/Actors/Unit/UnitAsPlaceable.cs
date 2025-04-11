using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsPlaceable : MonoBehaviour, IPlaceable
{
	#region DIRECT REF
	[field: SerializeField] public InterfaceReference<ISelectable> Selectable { get; private set; }
	[field: SerializeField] public InterfaceReference<IDamagable> Damagable { get; private set; }
	[field: SerializeField] public InterfaceReference<IAttacker> Attacker { get; private set; }
	#endregion

	#region INTERNAL VAR
	public List<Node> OccupyingNodes { get; private set; } = new();
	#endregion
	#region REF
	public Transform Transform => transform;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsPlaced { get; protected set; }
	#endregion

	public void Place()
	{
		IsPlaced = true;

		OccupyingNodes.ForEach(x =>
		{
			x.SetIsOccupied(true);
			x.SetInsidePlaceable(this);
		});
	}


	public void Deplace()
	{
		IsPlaced = false;

		OccupyingNodes.ForEach(x =>
		{
			x.SetIsOccupied(false);
			x.SetInsidePlaceable(null);
		});
	}


	public void SetOccupyingNodes(List<Node> occupyingNodes)
	{
		OccupyingNodes = occupyingNodes;
	}



}
