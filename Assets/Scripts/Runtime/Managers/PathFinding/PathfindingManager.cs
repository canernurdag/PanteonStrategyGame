using UnityEngine;
using System.Collections.Generic;

public class PathfindingManager : Singleton<PathfindingManager>
{
	private GridManager _gridManager;

	private void Start()
	{
		_gridManager = GridManager.Instance;
	}

	public List<Node> GetPath(Node startNode, Node targetNode)
	{

		List<Node> openSet = new List<Node> { startNode };
		HashSet<Node> closedSet = new HashSet<Node>();

		startNode.GCost = 0;
		startNode.HCost = GetDistance(startNode, targetNode);

		while (openSet.Count > 0)
		{

			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].FCost < currentNode.FCost ||
					(openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost))
				{
					currentNode = openSet[i];
				}
			}


			openSet.Remove(currentNode);
			closedSet.Add(currentNode);


			if (currentNode == targetNode)
			{
				return RetracePath(startNode, targetNode);
			}


			foreach (Node neighbor in _gridManager.GetNeighbours(currentNode))
			{
				if (neighbor.IsOccupied || closedSet.Contains(neighbor))
				{
					continue; 
				}

				int newGCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
				if (newGCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
				{

					neighbor.GCost = newGCostToNeighbor;
					neighbor.HCost = GetDistance(neighbor, targetNode);
					//neighbor.FCost = neighbor.GCost + neighbor.HCost;
					neighbor.Parent = currentNode;

					if (!openSet.Contains(neighbor))
					{
						openSet.Add(neighbor);
					}
				}
			}
		}

		return null;
	}



	
	private int GetDistance(Node nodeA, Node nodeB)
	{
		int distX = Mathf.Abs(nodeA.X - nodeB.X);
		int distY = Mathf.Abs(nodeA.Y - nodeB.Y);
		return distX + distY; 
	}


	private List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.Parent;
		}

		path.Reverse();
		return path;
	}

	public Node FindNearestUnoccupiedAndNoPreventNode(Node targetNode)
	{
		if (targetNode == null || _gridManager.Grid == null)
		{
			return null;
		}

		if (!targetNode.IsOccupied && !targetNode.PreventPlaceableSelection)
		{
			return targetNode;
		}

		Queue<Node> queue = new Queue<Node>();
		HashSet<Node> visited = new HashSet<Node>();

		queue.Enqueue(targetNode);
		visited.Add(targetNode);

		while (queue.Count > 0)
		{
			Node currentNode = queue.Dequeue();

	
			foreach (Node neighbor in _gridManager.GetNeighbours(currentNode))
			{
				if (visited.Contains(neighbor))
				{
					continue; 
				}

			
				visited.Add(neighbor);
				queue.Enqueue(neighbor);

				
				if (!neighbor.IsOccupied && !neighbor.PreventPlaceableSelection)
				{
					return neighbor;
				}
			}
		}

		return null;
	}
}
