using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationUiPresenter : MonoBehaviour
{
	#region DIRECT REFERENCES
	[SerializeField] private InformationUiModel _informationUiModel;
	[SerializeField] private InformationUiView _informationUiView;
	[SerializeField] private Transform _parentTransform;
	#endregion

	#region REF
	private OnBuildingSelected _onBuildingSelected;
	private OnBuildingDeselected _onBuildingDeselected;
	private OnProductCreateRequest _onProductCreateRequest;
	#endregion

	#region INTERNAL VARIABLES
	private int _productIndex = 0;
	#endregion

	private void Awake()
	{
		InactivateInformationPanel();
	}

	private void Start()
	{
		_onBuildingSelected = EventManager.Instance.GetEvent<OnBuildingSelected>();
		_onBuildingDeselected = EventManager.Instance.GetEvent<OnBuildingDeselected>();
	
		_onProductCreateRequest = EventManager.Instance.GetEvent<OnProductCreateRequest>();
	
		_onBuildingSelected.AddListener(Setup);
		_onBuildingDeselected.AddListener(InactivateInformationPanel);

	}

	private void OnDestroy()
	{
		_onBuildingSelected?.RemoveListener(Setup);
		_onBuildingDeselected.RemoveListener(InactivateInformationPanel);
	}

	public void Setup(Building building)
	{
		ActivateInformationPanel();

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
			UpdateViewSelectedProduct();
		}

	}


	public void SetLeftButtonFunction()
	{
		_informationUiView.SetLeftButtonFunction(() => 
		{
			_productIndex--;
			if (_productIndex < 0) _productIndex = _informationUiModel.SelectedBuilding.BuildingDataSO.ProductDatas.Count - 1;

			UpdateViewSelectedProduct();
		});
	}

	public void SetRightButtonFunction() 
	{
		_informationUiView.SetRightButtonFunction(() => 
		{
			_productIndex++;
			if (_productIndex > _informationUiModel.SelectedBuilding.BuildingDataSO.ProductDatas.Count - 1) _productIndex = 0;

			UpdateViewSelectedProduct();
		});
	}

	private void UpdateViewSelectedProduct()
	{
		var selectedProductData = _informationUiModel.SelectedBuilding.BuildingDataSO.ProductDatas[_productIndex];
		_informationUiView.SetProductImage(selectedProductData.Sprite);
		_informationUiView.SetProductButtonFunction(() => _onProductCreateRequest.Execute(_informationUiModel.SelectedBuilding, selectedProductData));
	}

	public void ActivateInformationPanel()
	{
		_parentTransform.gameObject.SetActive(true);
	}

	public void InactivateInformationPanel()
	{
		_parentTransform.gameObject.SetActive(false);
	}

}
