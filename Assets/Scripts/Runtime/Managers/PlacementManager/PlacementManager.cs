using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : Singleton<PlacementManager>
{
	#region REF
	private OnBuildingUiSelected _onBuildingUiSelected;
	#endregion
	private void Start()
	{
		_onBuildingUiSelected = EventManager.Instance.GetEvent<OnBuildingUiSelected>();
	}

	

	private void Update()
	{
		
	}
}
