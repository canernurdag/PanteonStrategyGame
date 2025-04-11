using UnityEngine;

public abstract class BuildingFactory : MonoBehaviour
{
	[field: SerializeField] public BuildingType BuildingType { get; protected set; }
	public abstract Building CreateBuilding();
	
}
