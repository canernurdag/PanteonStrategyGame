
using UnityEngine;

public class PowerPlantFactory : BuildingFactory
{
	#region DIRECT REF
	[SerializeField] private PowerPlant _powerPlantPrefab;
	#endregion
	public override IBuilding CreateBuilding()
	{
		return Instantiate(_powerPlantPrefab);
	}
}
