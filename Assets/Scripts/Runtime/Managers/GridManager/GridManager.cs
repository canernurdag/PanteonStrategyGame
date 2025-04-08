using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
	#region DIRECT REF
	[SerializeField] private NodeFactory _nodeFactory;
	#endregion
	public int width = 20;
	public int height = 20;
	public float cellSize = 1f;

	private Node[,] grid;

	protected override void Awake()
	{
		base.Awake();
		CreateGrid();
	}

	void CreateGrid()
	{
		grid = new Node[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Vector3 worldPos = new Vector3(x, y) * cellSize;
				grid[x, y] = _nodeFactory?.CreateNode();
				grid[x, y].Init(x, y, worldPos, transform);
			}
		}
	}

	public Node GetNodeFromWorldPosition(Vector3 worldPosition)
	{
		int x = Mathf.RoundToInt(worldPosition.x / cellSize);
		int y = Mathf.RoundToInt(worldPosition.y / cellSize);
		if (IsInBounds(x, y)) return grid[x, y];
		return null;
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();
		for (int dx = -1; dx <= 1; dx++)
		{
			for (int dy = -1; dy <= 1; dy++)
			{
				if (dx == 0 && dy == 0) continue;
				int checkX = node.X + dx;
				int checkY = node.Y + dy;
				if (IsInBounds(checkX, checkY))
					neighbours.Add(grid[checkX, checkY]);
			}
		}
		return neighbours;
	}

	public bool IsInBounds(int x, int y)
	{
		return x >= 0 && y >= 0 && x < width && y < height;
	}

}
