using UnityEngine;

public abstract class Building : MonoBehaviour
{
	[field: SerializeField]public BuildingDataSO BuildingDataSO { get; protected set; }
	public void SetBuildingDataSo(BuildingDataSO buildingDataSO)
	{
		BuildingDataSO = buildingDataSO;
	}
}
