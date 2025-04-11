using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAsSelectable : MonoBehaviour, ISelectable
{
	#region DIRECT REF
	[SerializeField] private Building _building;
	[field: SerializeField]public InterfaceReference<IPlaceable> Placeable { get; private set; }
	[SerializeField] private GameObject _selectionGo;
	#endregion

	#region REF
	private OnBuildingSelected _onBuildingSelected;
	private OnBuildingDeselected _onBuildingDeselected;
	public Transform Transform => transform;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsSelected { get; protected set; }


	#endregion

	private void Start()
	{
		_onBuildingSelected = EventManager.Instance.GetEvent<OnBuildingSelected>();
		_onBuildingDeselected = EventManager.Instance.GetEvent<OnBuildingDeselected>();
		_onBuildingDeselected.AddListener(Deselect);
	}

	private void OnDestroy()
	{
		_onBuildingDeselected.RemoveListener(Deselect);
	}

	public void Select()
	{
		IsSelected = true;
		_onBuildingSelected.Execute(_building);
		_selectionGo.SetActive(true);
	}
	public void Deselect()
	{
		IsSelected = false;
		_selectionGo.SetActive(false);
	}
}
