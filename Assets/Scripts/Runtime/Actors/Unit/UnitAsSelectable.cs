using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsSelectable : MonoBehaviour, ISelectable
{
	#region DIRECT REF
	[field:SerializeField] public Unit Unit { get; private set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> Placeable { get; private set; }
	#endregion

	#region REF
	private OnUnitSelected _onUnitSelected;
	public Transform Transform => transform;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsSelected { get; protected set; }
	#endregion

	private void Start()
	{
		_onUnitSelected = EventManager.Instance.GetEvent<OnUnitSelected>();
	}


	public void Select()
	{
		IsSelected = true;
		_onUnitSelected.Execute(Unit);
	}
	public void Deselect()
	{
		IsSelected = false;
	}
}
