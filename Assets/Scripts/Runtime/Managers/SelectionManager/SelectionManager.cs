using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : Singleton<SelectionManager>
{
	#region REF
	private OnLeftClickInputGiven _onLeftClickInputGiven;
	private OnRightClickInputGiven _onRightClickInputGiven;
	private OnBuildingDeselected _onBuildingDeselected;
	private OnPreventSelectionChanged _onPreventSelectionChanged;
	#endregion

	#region INSIDE VARIABLES
	private bool _isPreventSelection;
	#endregion

	private void Start()
	{
		_onLeftClickInputGiven = EventManager.Instance.GetEvent<OnLeftClickInputGiven>();
		_onRightClickInputGiven = EventManager.Instance.GetEvent<OnRightClickInputGiven>();
		_onBuildingDeselected = EventManager.Instance.GetEvent<OnBuildingDeselected>();
		_onPreventSelectionChanged = EventManager.Instance.GetEvent<OnPreventSelectionChanged>();

		_onLeftClickInputGiven.AddListener(HandleLeftClick);
		_onRightClickInputGiven.AddListener(HandleRightClick);
		_onPreventSelectionChanged.AddListener(SetPreventSelection);
	}

	private void OnDestroy()
	{
		_onLeftClickInputGiven.RemoveListener(HandleLeftClick);
		_onRightClickInputGiven.RemoveListener(HandleRightClick);
		_onPreventSelectionChanged.RemoveListener(SetPreventSelection);
	}

	public void HandleLeftClick(Vector3 inputPosition)
	{
		if (_isPreventSelection) return;

		bool isUiClick = EventSystem.current.IsPointerOverGameObject();
		if(isUiClick) return; 

		Node clickNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (clickNode == null)
		{
			_onBuildingDeselected.Execute();
			return;
		}


		var insidePlaceable = clickNode.InsidePlaceable;
		if (insidePlaceable == null) 
		{
			_onBuildingDeselected.Execute();
			return;
		}

		var insideSelectable = insidePlaceable.Selectable.Value;
		if (insideSelectable == null)
		{
			_onBuildingDeselected.Execute();
			return;
		}

		insideSelectable.Select();
	}

	private void HandleRightClick(Vector3 inputPosition)
	{
		//FOR POTENTIAL FUTURE REQUESTS
	}

	public void SetPreventSelection(bool isPrevent)
	{
		_isPreventSelection = isPrevent;
	}
}
