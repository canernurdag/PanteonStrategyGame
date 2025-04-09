using UnityEngine;

public abstract class BuildingFactory : MonoBehaviour
{
	public abstract IBuilding CreateBuilding();
}
