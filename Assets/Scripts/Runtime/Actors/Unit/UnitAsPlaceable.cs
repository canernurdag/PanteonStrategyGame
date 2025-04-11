using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsPlaceable : MonoBehaviour, IPlaceable
{
	#region DIRECT REF
	[field: SerializeField] public InterfaceReference<ISelectable> Selectable { get; private set; }
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

	public void SetAsPlaced()
	{
		IsPlaced = true;

		OccupyingNodes.ForEach(x =>
		{
			x.SetIsOccupied(true);
			x.SetInsidePlaceable(this);
		});
	}


	public void SetOccupyingNodes(List<Node> occupyingNodes)
	{
		OccupyingNodes = occupyingNodes;
	}



}
