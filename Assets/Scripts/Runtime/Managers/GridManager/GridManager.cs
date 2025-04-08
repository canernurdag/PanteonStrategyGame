using System.Collections.Generic;
using UnityEngine;

public sealed class GridManager : Singleton<GridManager>
{
	#region DIRECT REF
	[SerializeField] private NodeFactory _nodeFactory;
	#endregion

	#region INTERNAL VARIABLES
	private OnGridCenterPositionDetermined _onGridCenterPositionDetermined;


	public int width = 20;
	public int height = 20;
	public float cellSize = 1f;

	private Node[,] _grid;
	private Vector3 _gridCenterPosition;

	#endregion

	protected override void Awake()
	{
		base.Awake();
		CreateGrid();

		
	}

	private void Start()
	{
		_onGridCenterPositionDetermined = EventManager.Instance.GetEvent<OnGridCenterPositionDetermined>();

		_gridCenterPosition = GetGridCenterPosition();
		_onGridCenterPositionDetermined.Execute(_gridCenterPosition);
	}



	private void CreateGrid()
	{
		_grid = new Node[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Vector3 worldPos = new Vector3(x, y) * cellSize;
				_grid[x, y] = _nodeFactory?.CreateNode();
				_grid[x, y].Init(x, y, worldPos, transform);
			}
		}
	}

	private Vector3 GetGridCenterPosition()
	{
		Vector3 centerPos = Vector3.zero;
		int width = _grid.GetLength(0);
		int height = _grid.GetLength(1);

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				var node = _grid[i, j];
				centerPos += node.transform.position;
			}
		}

		int totalNodeCount = width * height;
		return centerPos / (float)totalNodeCount;
	}

	public Node GetNodeFromWorldPosition(Vector3 worldPosition)
	{
		int x = Mathf.RoundToInt(worldPosition.x / cellSize);
		int y = Mathf.RoundToInt(worldPosition.y / cellSize);
		if (IsInBounds(x, y)) return _grid[x, y];
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
					neighbours.Add(_grid[checkX, checkY]);
			}
		}
		return neighbours;
	}

	public bool IsInBounds(int x, int y)
	{
		return x >= 0 && y >= 0 && x < width && y < height;
	}

}
