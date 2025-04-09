using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUiFactory : MonoBehaviour
{
	#region DIRECT REFERENCE
	[SerializeField] private BuildingUiPresenter _buildingUiPresenterPrefab;
	#endregion

	public BuildingUiPresenter CreateBuildingUi()
	{
		return Instantiate(_buildingUiPresenterPrefab);
	}
}
