using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsSelectable : MonoBehaviour, ISelectable
{
	#region DIRECT REF
	[field:SerializeField] public Unit Unit { get; private set; }
	[field: SerializeField] public InterfaceReference<IPlaceable> Placeable { get; private set; }
	[SerializeField] private GameObject _selectionGo;
	#endregion

	#region REF
	private OnUnitSelected _onUnitSelected;
	private OnUnitDeselected _onUnitDeselected;
	public Transform Transform => transform;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsSelected { get; protected set; }
	#endregion

	private void Start()
	{
		_onUnitSelected = EventManager.Instance.GetEvent<OnUnitSelected>();
		_onUnitDeselected = EventManager.Instance.GetEvent<OnUnitDeselected>();
		_onUnitDeselected.AddListener(Deselect);
	}

	private void OnDestroy()
	{
		_onUnitDeselected.RemoveListener(Deselect);
	}

	public void Select()
	{
		IsSelected = true;
		_onUnitSelected.Execute(Unit);
		_selectionGo.SetActive(true);
	}
	public void Deselect()
	{
		IsSelected = false;
		_selectionGo.SetActive(false);
	}
}
