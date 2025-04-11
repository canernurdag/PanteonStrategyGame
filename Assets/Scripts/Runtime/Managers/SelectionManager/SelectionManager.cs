using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : Singleton<SelectionManager>
{
	#region REF
	private OnLeftClickInputGiven _onLeftClickInputGiven;
	private OnRightClickInputGiven _onRightClickInputGiven;

	private OnBuildingSelected _onBuildingSelected;
	private OnBuildingDeselected _onBuildingDeselected;

	private OnUnitSelected _onUnitSelected;
	private OnUnitDeselected _onUnitDeselected;

	private OnFlagSelected _onFlagSelected;
	private OnFlagDeselected _onFlagDeselected;
	
	private OnPreventSelectionChanged _onPreventSelectionChanged;
	private OnUnitMoveCommand _onUnitMoveCommand;
	#endregion

	#region INSIDE VARIABLES
	private bool _isPreventSelection;
	private ISelectable _selectedFlagSpawnPoint;
	private ISelectable _selectedBuilding;
	private ISelectable _selectedUnit;
	#endregion

	private void Start()
	{
		_onLeftClickInputGiven = EventManager.Instance.GetEvent<OnLeftClickInputGiven>();
		_onRightClickInputGiven = EventManager.Instance.GetEvent<OnRightClickInputGiven>();
		_onPreventSelectionChanged = EventManager.Instance.GetEvent<OnPreventSelectionChanged>();

		_onBuildingSelected = EventManager.Instance.GetEvent<OnBuildingSelected>();
		_onBuildingDeselected = EventManager.Instance.GetEvent<OnBuildingDeselected>();

		_onUnitSelected = EventManager.Instance.GetEvent<OnUnitSelected>();
		_onUnitDeselected = EventManager.Instance.GetEvent<OnUnitDeselected>();

		_onFlagSelected = EventManager.Instance.GetEvent<OnFlagSelected>();
		_onFlagDeselected = EventManager.Instance.GetEvent<OnFlagDeselected>();

		_onUnitMoveCommand = EventManager.Instance.GetEvent<OnUnitMoveCommand>();

		_onLeftClickInputGiven.AddListener(HandleLeftClick);
		_onRightClickInputGiven.AddListener(HandleRightClick);
		_onPreventSelectionChanged.AddListener(SetPreventSelection);

		_onBuildingSelected.AddListener(SetSelectedBuilding);
		_onUnitSelected.AddListener(SetSelectedUnit);
		_onFlagSelected.AddListener(SetSelectedFlag);
	}

	private void OnDestroy()
	{
		_onLeftClickInputGiven.RemoveListener(HandleLeftClick);
		_onRightClickInputGiven.RemoveListener(HandleRightClick);
		_onPreventSelectionChanged.RemoveListener(SetPreventSelection);

		_onBuildingSelected.RemoveListener(SetSelectedBuilding);
		_onUnitSelected.RemoveListener(SetSelectedUnit);
		_onFlagSelected.RemoveListener(SetSelectedFlag);
	}

	public void HandleLeftClick(Vector3 inputPosition)
	{
		if (_isPreventSelection) return;

		bool isUiClick = EventSystem.current.IsPointerOverGameObject();
		if(isUiClick) return; 

		Node clickNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (clickNode == null)
		{
			ResetSelectedBuildingAndUnit();
			return;
		}

		var nodeInsidePlaceable = clickNode.InsidePlaceable;
		if (nodeInsidePlaceable == null) 
		{
			ResetSelectedBuildingAndUnit();

			var nodeInsideFlag = clickNode.InsideFlagSpawnPoint;

			if(nodeInsideFlag == null)
			{
				if (_selectedFlagSpawnPoint != null)
				{
					//DESELECT
					_selectedFlagSpawnPoint.Deselect();
					_selectedFlagSpawnPoint = null;
				}
				else if (_selectedFlagSpawnPoint == null)
				{
					return;
				}
			}
			else if(nodeInsideFlag != null)
			{
				if (_selectedFlagSpawnPoint != null)
				{
				
					return;
				}
				else if (_selectedFlagSpawnPoint == null)
				{
					//SELECT
					nodeInsideFlag.Selectable.Value.Select();
					return;
				}
			}
		}
		else if(nodeInsidePlaceable != null)
		{
			if (_selectedFlagSpawnPoint != null) return;

			var insideSelectable = nodeInsidePlaceable.Selectable.Value;
			if (insideSelectable == null)
			{
				ResetSelectedBuildingAndUnit();

				return;
			}

			insideSelectable.Select();
		}

		
	}



	private void HandleRightClick(Vector3 inputPosition)
	{
		//FOR POTENTIAL FUTURE REQUESTS
		if(_selectedUnit != null)
		{
			var unitAsSelectable = _selectedUnit as UnitAsSelectable;
			var unit = unitAsSelectable.Unit;

			Node clickNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);

			_onUnitMoveCommand.Execute(unit, clickNode);
		}
	}

	public void SetPreventSelection(bool isPrevent)
	{
		_isPreventSelection = isPrevent;
	}

	public void SetSelectedBuilding(Building building)
	{
		_selectedBuilding = building.BuildingAsSelectable.Value;
	}

	public void SetSelectedUnit(Unit unit)
	{
		_selectedUnit = unit.UnitAsSelectable.Value; ;
	}

	public void SetSelectedFlag(FlagSpawnPoint flagSpawnPoint)
	{
		_selectedFlagSpawnPoint = flagSpawnPoint;
	}

	private void ResetSelectedBuildingAndUnit()
	{
		_onBuildingDeselected.Execute();
		_onUnitDeselected.Execute();
		_selectedBuilding = null;
		_selectedUnit = null;
	}
}
