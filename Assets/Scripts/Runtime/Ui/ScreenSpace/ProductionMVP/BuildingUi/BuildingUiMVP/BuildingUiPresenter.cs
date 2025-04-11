using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUiPresenter : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private BuildingUiView _buildingUiView;
	[SerializeField] private BuildingUiModel _buildingUiModel;
	#endregion

	#region REF
	private OnBuildingUiSelected _onBuildingUiSelected;
	private OnBuildingDeselected _onBuildingDeselected;
	private OnPreventSelectionChanged _onPreventSelectionChanged;
	#endregion

	private void Start()
	{
		_onBuildingUiSelected = EventManager.Instance.GetEvent<OnBuildingUiSelected>();
		_onBuildingDeselected = EventManager.Instance.GetEvent<OnBuildingDeselected>();
		_onPreventSelectionChanged = EventManager.Instance.GetEvent<OnPreventSelectionChanged>();
	}

	public void Setup(BuildingDataSO buildingDataSO)
	{
		_buildingUiModel.SetBuildingDataSO(buildingDataSO);
		_buildingUiView.SetInsideImage(buildingDataSO.Sprite);
		_buildingUiView.SetButtonFunction(() => 
		{
			_onBuildingUiSelected.Execute(buildingDataSO);
			_onBuildingDeselected.Execute();
			_onPreventSelectionChanged.Execute(true);
		});
	}

	
}
