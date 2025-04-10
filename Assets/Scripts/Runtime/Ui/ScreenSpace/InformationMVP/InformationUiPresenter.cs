using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationUiPresenter : MonoBehaviour
{
	#region DIRECT REFERENCES
	[SerializeField] private InformationUiModel _informationUiModel;
	[SerializeField] private InformationUiView _informationUiView;
	#endregion

	#region REF
	private OnBuildingSelected _onBuildingSelected;
	private OnProductCreateRequest _onProductCreateRequest;
	#endregion

	#region INTERNAL VARIABLES
	private int _productIndex = 0;
	#endregion

	private void Start()
	{
		_onBuildingSelected = EventManager.Instance.GetEvent<OnBuildingSelected>();
		_onProductCreateRequest = EventManager.Instance.GetEvent<OnProductCreateRequest>();
		_onBuildingSelected.AddListener(Setup);
	}

	private void OnDestroy()
	{
		_onBuildingSelected?.RemoveListener(Setup);
	}

	public void Setup(Building building)
	{
		var buildingDataSO = building.BuildingDataSO;
		_productIndex = 0;

		_informationUiModel.SetSelectedBuilding(building);
		_informationUiView.SetBuildingImage(buildingDataSO.Sprite);
		_informationUiView.SetBuildingName(buildingDataSO.Name);

		if (buildingDataSO.ProductDatas.Count == 0)
		{
			_informationUiView.InactivateProductDivision();
		}
		else if (buildingDataSO.ProductDatas.Count > 0)
		{
			_informationUiView.ActivateProductDivision();
			SetLeftButtonFunction();
			SetRightButtonFunction();
		}

	}


	public void SetLeftButtonFunction()
	{
		_productIndex--;
		if( _productIndex < 0 ) _productIndex = _informationUiModel.SelectedBuilding.BuildingDataSO.ProductDatas.Count-1;

		UpdateViewSelectedProduct();


	}

	public void SetRightButtonFunction() 
	{
		_productIndex++;
		if (_productIndex > _informationUiModel.SelectedBuilding.BuildingDataSO.ProductDatas.Count - 1) _productIndex = 0;

		UpdateViewSelectedProduct();
	}

	private void UpdateViewSelectedProduct()
	{
		var selectedProductData = _informationUiModel.SelectedBuilding.BuildingDataSO.ProductDatas[_productIndex];
		_informationUiView.SetBuildingName(selectedProductData.Name);
		_informationUiView.SetBuildingImage(selectedProductData.Sprite);
		_informationUiView.SetProductButtonFunction(() => _onProductCreateRequest.Execute(_informationUiModel.SelectedBuilding, selectedProductData));
	}
}
