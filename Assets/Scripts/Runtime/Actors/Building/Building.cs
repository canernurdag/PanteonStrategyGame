using UnityEngine;

public abstract class Building : MonoBehaviour
{
	#region DIRECT REF
	[field: SerializeField] public BuildingDataSO BuildingDataSO { get; protected set; }
	[field: SerializeField] public InterfaceReference<ISelectable> BuildingAsSelectable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> BuildingAsPlaceable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IUnitProducer> BuildingAsUnitProducer { get; protected set; }

	#endregion


	public void SetBuildingDataSo(BuildingDataSO buildingDataSO)
	{
		BuildingDataSO = buildingDataSO;
	}

}
