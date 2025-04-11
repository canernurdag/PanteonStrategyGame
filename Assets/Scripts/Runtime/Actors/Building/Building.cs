using UnityEngine;

public abstract class Building : MonoBehaviour
{
	#region DIRECT REF
	[field: SerializeField] public BuildingDataSO BuildingDataSO { get; protected set; }
	[field: SerializeField] public BuildingSelectable BuildingSelectable { get; protected set; }
	[field: SerializeField] public BuildingPlaceable BuildingPlaceable { get; protected set; }
	[field: SerializeField] public InterfaceReference<ISelectable> Selectable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> Placeable { get; protected set; }
	#endregion


	public void SetBuildingDataSo(BuildingDataSO buildingDataSO)
	{
		BuildingDataSO = buildingDataSO;
	}

}
