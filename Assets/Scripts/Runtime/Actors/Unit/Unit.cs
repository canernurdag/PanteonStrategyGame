using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	#region DIRECT REF
	[field: SerializeField] public UnitDataSO UnitDataSO { get; protected set; }
	[field: SerializeField] public UnitSelectable UnitSelectable { get; protected set; }
	[field: SerializeField] public InterfaceReference<IAttacking> UnitAttacking { get; protected set; }
	[field: SerializeField] public InterfaceReference<ISelectable> Selectable { get; protected set; }
	#endregion


	public void SetUnitDataSo(UnitDataSO unitDataSO)
	{
		UnitDataSO = unitDataSO;
	}
}
