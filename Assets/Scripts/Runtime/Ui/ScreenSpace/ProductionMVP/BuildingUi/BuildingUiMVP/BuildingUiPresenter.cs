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
	#endregion

	private void Start()
	{
		_onBuildingUiSelected = EventManager.Instance.GetEvent<OnBuildingUiSelected>();
	}

	public void Setup(BuildingDataSO buildingDataSO)
	{
		_buildingUiModel.SetBuildingDataSO(buildingDataSO);
		_buildingUiView.SetInsideImage(buildingDataSO.Sprite);
		_buildingUiView.SetButtonFunction(() => 
		{
			_onBuildingUiSelected.Execute(buildingDataSO);
		});
	}

	
}
