using Lean.Pool;
using UnityEngine;

public abstract class Building : MonoBehaviour, IPoolable
{
	#region DIRECT REF
	[field: SerializeField] public BuildingDataSO BuildingDataSO { get; protected set; }
	[field: SerializeField] public InterfaceReference<ISelectable> BuildingAsSelectable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> BuildingAsPlaceable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IUnitProducer> BuildingAsUnitProducer { get; protected set; }
	[field: SerializeField] public InterfaceReference<IDamagable> Damagable { get; protected set; } 
	#endregion

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		Damagable.Value.SetInitHealth(BuildingDataSO.InitHealth);
		Damagable.Value.SetCurrentHealth(BuildingDataSO.InitHealth);
	}

	public void SetBuildingDataSo(BuildingDataSO buildingDataSO)
	{
		BuildingDataSO = buildingDataSO;
	}

	public void OnSpawn()
	{
		Init();
	}

	public void OnDespawn()
	{

	}
}
