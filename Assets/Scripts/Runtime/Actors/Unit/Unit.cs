using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	#region DIRECT REF
	[field: SerializeField] public UnitDataSO UnitDataSO { get; protected set; }
	[field: SerializeField] public InterfaceReference<IAttacker> UnitAsAttacker { get; protected set; }
	[field: SerializeField] public InterfaceReference<ISelectable> UnitAsSelectable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IUnitMovable> UnitAsMovable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> UnitAsPlaceable { get; protected set; }	
	#endregion


	public void SetUnitDataSo(UnitDataSO unitDataSO)
	{
		UnitDataSO = unitDataSO;
	}
}
