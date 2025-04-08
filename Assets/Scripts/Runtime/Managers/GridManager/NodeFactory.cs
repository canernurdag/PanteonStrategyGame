using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private Node _nodePrefab;
	#endregion

	public Node CreateNode()
	{
		Node node = Instantiate(_nodePrefab);
		return node;
	}
}
