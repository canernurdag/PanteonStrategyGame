using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelectable : MonoBehaviour, ISelectable
{
	#region DIRECT REF
	[SerializeField] private Building _building;
	#endregion

	#region REF
	private OnBuildingSelected _onBuildingSelected;
	public Transform Transform => transform;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsSelected { get; protected set; }
	#endregion

	private void Start()
	{
		_onBuildingSelected = EventManager.Instance.GetEvent<OnBuildingSelected>();
	}

	public void Select()
	{
		IsSelected = true;
		_onBuildingSelected.Execute(_building);
	}
	public void Deselect()
	{
		IsSelected = false;
	}
}
