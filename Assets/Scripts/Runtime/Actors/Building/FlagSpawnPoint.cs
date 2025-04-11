using System.Collections.Generic;
using UnityEngine;

public class FlagSpawnPoint : MonoBehaviour, ISelectable, IPlaceable
{
	#region DIRECT REF
	[field: SerializeField] public Building Building { get; private set; }
	[field: SerializeField] public InterfaceReference<ISelectable> Selectable { get; private set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> Placeable { get; private set; }
	#endregion

	#region REF
	public Transform Transform => transform;
	private OnMouseWorldPositionGiven _onMouseWorldPositionGiven;
	private OnFlagSelected _onFlagSelected;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsSelected { get; protected set; }
	public List<Node> OccupyingNodes { get; private set; } = new();
	public bool IsPlaced { get; protected set; }
	#endregion

	private void Start()
	{
		_onMouseWorldPositionGiven = EventManager.Instance.GetEvent<OnMouseWorldPositionGiven>();
		_onFlagSelected = EventManager.Instance.GetEvent<OnFlagSelected>();

		_onMouseWorldPositionGiven.AddListener(SetFlagPosition);
	}

	private void OnDestroy()
	{
		_onMouseWorldPositionGiven.RemoveListener(SetFlagPosition);
	}


	public void SetFlagPosition(Vector3 inputPosition)
	{
		if (!IsSelected) return;

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (followNode == null) return;

		transform.position = followNode.transform.position;
	}


	public void Select()
	{
		IsSelected = true;
		_onFlagSelected.Execute(this);
		Deplace();
	}
	public void Deselect()
	{
		IsSelected = false;
		Place();
		
	}

	public void Place(Node node)
	{
		IsPlaced = true;

		transform.position = node.transform.position;
		OccupyingNodes = new() { node };
		
		OccupyingNodes.ForEach(x =>
		{
			x.SetInsideFlagSpawnPoint(this);

		});

	}

	public void Place()
	{
		IsPlaced = true;

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(transform.position);
		if (followNode == null) return;

		transform.position = followNode.transform.position;
		OccupyingNodes = new() { followNode };
		OccupyingNodes.ForEach(x =>
		{
			x.SetInsideFlagSpawnPoint(this);

		});

	}

	public void Deplace()
	{
		IsPlaced = false;
		OccupyingNodes.ForEach(x => x.SetInsideFlagSpawnPoint(null));
	}

	public void SetBuilding(Building building)
	{
		Building = building;
	}

	public void SetOccupyingNodes(List<Node> occupyingNodes)
	{
		OccupyingNodes = occupyingNodes;
	}



}
