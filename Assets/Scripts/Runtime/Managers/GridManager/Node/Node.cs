using UnityEngine;

public class Node : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private NodeVisualController _nodeVisualController;
	#endregion

	#region INTERNAL NODE VARIABLES
	public int X { get; private set; } = 0;
	public int Y { get; private set; } = 0;
	public Vector3 WorldPosition { get; private set; } = Vector3.zero;
	public Node Parent { get; set; }
	public bool IsOccupied { get; private set; } = false;
	public IPlaceable InsidePlaceable { get; private set; } = null;
	public IPlaceable InsideFlagSpawnPoint { get; private set; } = null;
	#endregion

	#region A*PATHFINDING
	public int GCost { get; set; } = 0;
	public int HCost { get; set; } = 0;
	public int FCost => GCost + HCost;
	#endregion
	public void Init(int x, int y, Vector3 worldPos, Transform parent)
	{
		X = x;
		Y = y;
		WorldPosition = worldPos;
		transform.position = WorldPosition;
		transform.SetParent(parent);
	}
	
	public void SetIsOccupied(bool isOccupied)
	{
		IsOccupied = isOccupied;
	}

	public void SetInsidePlaceable(IPlaceable placeable)
	{
		InsidePlaceable = placeable;
	}

	public void SetInsideFlagSpawnPoint(FlagSpawnPoint flagSpawnPoint)
	{
		InsideFlagSpawnPoint = flagSpawnPoint;
	}

	public void ResetNodeVisual()
	{
		if(IsOccupied)
		{
			_nodeVisualController.SetNodeVisualAsPlaced();
		}
		else if(!IsOccupied)
		{
			_nodeVisualController.SetNodeVisualAsNormal();
		}
	}

	public void SetNodeVisualAsUnoccupied()
	{
		_nodeVisualController.SetNodeVisualAsUnoccupied();
	}

	public void SetNodeVisualAsOccupied()
	{
		_nodeVisualController.SetNodeVisualAsOccupied();
	}


}
