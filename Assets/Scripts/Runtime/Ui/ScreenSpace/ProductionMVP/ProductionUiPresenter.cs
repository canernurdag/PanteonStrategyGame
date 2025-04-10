using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProductionUiPresenter : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private ProductionUiView _productionView;
	[SerializeField] private ProductionUiModel _productionModel;
	[SerializeField] private BuildingUiFactory _buildingUiFactory;
	#endregion

	private List<BuildingUiPresenter> _buildingUiPresenters = new();

	#region INFINITE SCROLL
	private float _scrollHeight = 0f;
	private float _topThresholdPositionY;
	private float _bottomThresholdPositionY;
	private Vector2 _lastPosition;
	private bool _isDragUpward;
	#endregion

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		//Get Scroll Height
		_scrollHeight = _productionView.ScroolRect.GetComponent<RectTransform>().rect.height;
		
		//Get Row Count By ScrollHeight
		var rowCount = Mathf.CeilToInt(_scrollHeight / (_productionModel.ProductionUiDesignDataSO.ItemDimensionsInPixels.y + _productionModel.ProductionUiDesignDataSO.ItemSpaceY));

		//Create Building Uis
		CreateBuildingUis(rowCount);

		//Fill The Building Uis
		FillTheBuildingUis();

		//Set Threshold Positions To Check Whether The First Item Inside or not.
		SetThresholdPositionsY();

		Canvas.ForceUpdateCanvases();

		//Disable GridLayoutGroup Inorder To Item's Manual Positioning
		_productionView.GridLayoutGroup.enabled = false;
	}


	private void OnEnable()
	{
		_productionView.ScroolRect.onValueChanged.AddListener(OnScrollRectValueChanged);
	}

	private void OnDisable()
	{
		_productionView.ScroolRect.onValueChanged.RemoveListener(OnScrollRectValueChanged);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_lastPosition = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		var currentPosition = eventData.position;
		_isDragUpward = currentPosition.y > _lastPosition.y;
		_lastPosition = currentPosition;
	}

	private void OnScrollRectValueChanged(Vector2 value)
	{
		//For Better Readability
		var coloumnCount = _productionModel.ProductionUiDesignDataSO.ColoumnCount;

		var firstItems = _isDragUpward ? _buildingUiPresenters.GetRange(0, coloumnCount) :
			_buildingUiPresenters.GetRange(_buildingUiPresenters.Count - coloumnCount, coloumnCount);

		bool isFirstItemOutOfBounds = IsItemOutOfBounds(firstItems[0].transform);
		if (!isFirstItemOutOfBounds) return;

		var lastItems =_isDragUpward ? _buildingUiPresenters.GetRange(_buildingUiPresenters.Count - coloumnCount, coloumnCount) :
			_buildingUiPresenters.GetRange(0, coloumnCount);

		for (int i = 0; i < firstItems.Count; i++)
		{
			var firstItem = firstItems[i];
			var lastItem = lastItems[i];

			var lastItemPosition = lastItem.transform.position;
			var firstItemTargetPosition = lastItemPosition;

			//For Better Readability
			var itemDimensionsInPixels = _productionModel.ProductionUiDesignDataSO.ItemDimensionsInPixels;
			var itemSpaceY = _productionModel.ProductionUiDesignDataSO.ItemSpaceY;

			if (_isDragUpward)
			{
				firstItemTargetPosition.y = lastItemPosition.y - itemDimensionsInPixels.y - itemSpaceY;
				firstItem.transform.SetAsLastSibling();
				_buildingUiPresenters.RemoveAt(0);
				_buildingUiPresenters.Add(firstItem);
			}
			else if (!_isDragUpward)
			{
				firstItemTargetPosition.y = lastItemPosition.y + itemDimensionsInPixels.y + itemSpaceY;
				firstItem.transform.SetAsFirstSibling();
				_buildingUiPresenters.RemoveAt(_buildingUiPresenters.Count-1);
				_buildingUiPresenters.Insert(i, firstItem);
			}

			firstItem.transform.position = firstItemTargetPosition;

		}

	}

	private bool IsItemOutOfBounds(Transform item)
	{
		//For Better Readability
		var itemDimensionsInPixels = _productionModel.ProductionUiDesignDataSO.ItemDimensionsInPixels;

		return _isDragUpward?
			item.position.y - itemDimensionsInPixels.y*0.5f > _topThresholdPositionY:
			item.position.y + itemDimensionsInPixels.y*0.5f < _bottomThresholdPositionY;
			
	}

	private void SetThresholdPositionsY()
	{
		_topThresholdPositionY = _productionView.ScroolRect.transform.position.y + _scrollHeight * 0.5f - _productionModel.ProductionUiDesignDataSO.OutThreshold;
		_bottomThresholdPositionY = _productionView.ScroolRect.transform.position.y - _scrollHeight * 0.5f + _productionModel.ProductionUiDesignDataSO.OutThreshold;
	}

	private void CreateBuildingUis(int rowCount)
	{
		for (int i = 0; i < rowCount * _productionModel.ProductionUiDesignDataSO.ColoumnCount; i++)
		{
			var buildingUi = _buildingUiFactory.CreateBuildingUi();
			buildingUi.transform.SetParent(_productionView.ContentTransform);
			_buildingUiPresenters.Add(buildingUi);
		}
	}

	private void FillTheBuildingUis()
	{
		var activeBuildings = _productionModel.ProductionBuildingDataSO.ActiveBuildingDatas;

		for (int i = 0; i < _buildingUiPresenters.Count; i++)
		{
			var buildingPresenter = _buildingUiPresenters[i];
			var selectedBuildingIndex = i %  activeBuildings.Count;
			var selectedBuilding = activeBuildings[selectedBuildingIndex];

			buildingPresenter.Setup(selectedBuilding);
		}
	}


	
}
