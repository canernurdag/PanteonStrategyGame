using Lean.Pool;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IPoolable
{
	#region DIRECT REF
	[field: SerializeField] public UnitDataSO UnitDataSO { get; protected set; }
	[field: SerializeField] public InterfaceReference<IAttacker> UnitAsAttacker { get; protected set; }
	[field: SerializeField] public InterfaceReference<ISelectable> UnitAsSelectable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IUnitMovable> UnitAsMovable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> UnitAsPlaceable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IDamagable> Damagable { get; protected set; }
	#endregion

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		Damagable.Value.SetInitHealth(UnitDataSO.InitHealth);
		Damagable.Value.SetCurrentHealth(UnitDataSO.InitHealth);
		UnitAsAttacker.Value.SetDamageAmount(UnitDataSO.DamagePerAttack);
	}
	public void SetUnitDataSo(UnitDataSO unitDataSO)
	{
		UnitDataSO = unitDataSO;
	}

	public void OnSpawn()
	{
		Init();
	}

	public void OnDespawn()
	{
		
	}
}
