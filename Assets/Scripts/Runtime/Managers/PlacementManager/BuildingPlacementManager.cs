using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacementManager : Singleton<BuildingPlacementManager>
{
	#region DIRECT REF
	[SerializeField] private List<BuildingFactory> _buildingFactories = new();
	#endregion

	#region REF
	private OnBuildingUiSelected _onBuildingUiSelected;
	private OnMouseWorldPositionGiven _onMouseWorldPositionGiven;
	private OnLeftClickInputGiven _onLeftClickInputGiven;
	private OnRightClickInputGiven _onRightClickInputGiven;
	private OnPreventSelectionChanged _onPreventSelectionChanged;
	private OnBuildingPlaced _onBuildingPlaced;
	#endregion

	#region INTERNAL VAR
	private Building _candidateBuilding;
	private bool _isCandidateExist;
	private List<Node> _candidateBuildingNodes = new();
	#endregion

	private void Start()
	{
		_onBuildingUiSelected = EventManager.Instance.GetEvent<OnBuildingUiSelected>();
		_onMouseWorldPositionGiven = EventManager.Instance.GetEvent<OnMouseWorldPositionGiven>();
		_onLeftClickInputGiven = EventManager.Instance.GetEvent<OnLeftClickInputGiven>();
		_onRightClickInputGiven = EventManager.Instance.GetEvent<OnRightClickInputGiven>();
		_onPreventSelectionChanged = EventManager.Instance.GetEvent<OnPreventSelectionChanged>();
		_onBuildingPlaced = EventManager.Instance.GetEvent<OnBuildingPlaced>();

		_onBuildingUiSelected.AddListener(ExecutePlacementCandidateBuildingCreateSequence);
		_onMouseWorldPositionGiven.AddListener(SetCandidatePosition);
		_onMouseWorldPositionGiven.AddListener(ManipulateNodesUnderCandidate);
		_onLeftClickInputGiven.AddListener(PlaceCandidate);
		_onRightClickInputGiven.AddListener(CancelLastCandidate);
	}


	private void OnDestroy()
	{
		_onBuildingUiSelected.RemoveListener(ExecutePlacementCandidateBuildingCreateSequence);
		_onMouseWorldPositionGiven.RemoveListener(SetCandidatePosition);
		_onMouseWorldPositionGiven.RemoveListener(ManipulateNodesUnderCandidate);
		_onLeftClickInputGiven.RemoveListener(PlaceCandidate);
		_onRightClickInputGiven.RemoveListener(CancelLastCandidate);
	}

	public void ExecutePlacementCandidateBuildingCreateSequence(BuildingDataSO selectedBuildingData)
	{
		DestroyLastCandidate();
		
		Building placementCandidateBuilding = CreateNewCandidate(selectedBuildingData);
		SetCandiateBuilding(placementCandidateBuilding);
	}
	private void DestroyLastCandidate()
	{
		if (!_isCandidateExist) return;

		LeanPool.Despawn(_candidateBuilding);
		ResetCandidateVariables();
	}

	private void ResetCandidateVariables()
	{
		_isCandidateExist = false;
		_candidateBuilding = null;
		_candidateBuildingNodes = new();
	}
	private Building CreateNewCandidate(BuildingDataSO selectedBuildingData)
	{
		var buildingFactory = GetBuildingFactoryByBuildingType(selectedBuildingData.BuildingType);
		var placementCandidateBuilding = buildingFactory.CreateBuilding();
		return placementCandidateBuilding;
	}
	private BuildingFactory GetBuildingFactoryByBuildingType(BuildingType buildingType)
	{
		return _buildingFactories.FirstOrDefault(x=>x.BuildingType == buildingType);
	}
	public void SetCandiateBuilding(Building building)
	{
		_candidateBuilding = building;
		_isCandidateExist = _candidateBuilding != null;
	}

	private void ResetCandidatePreviousNodes()
	{
		if (_candidateBuildingNodes == null) return;
		_candidateBuildingNodes.ForEach(x =>x.ResetNodeVisual());
	}

	public void SetCandidatePosition(Vector3 inputPosition)
	{
		if (!_isCandidateExist) return;

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (followNode == null) return;

		_candidateBuilding.transform.position = followNode.transform.position
			+ Vector3.right * 0.5f
			+ Vector3.up * 0.5f;

	}
	public void ManipulateNodesUnderCandidate(Vector3 inputPosition)
	{
		if (!_isCandidateExist) return;

		ResetCandidatePreviousNodes();

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (followNode == null) return;

		_candidateBuildingNodes = GridManager.Instance.GetNodesByBuilding(followNode, _candidateBuilding);
		if(_candidateBuildingNodes == null) return;

		bool canBePlaced = CanBePlaced(followNode);
		if (canBePlaced)
		{
			_candidateBuildingNodes.ForEach(x => x.SetNodeVisualAsUnoccupied());
		}
		else if (!canBePlaced)
		{
			_candidateBuildingNodes.ForEach(x => x.SetNodeVisualAsOccupied());
		}	
	}



	public void PlaceCandidate(Vector3 inputPosition)
	{
		if (!_isCandidateExist) return;

		bool isUiClick = EventSystem.current.IsPointerOverGameObject();
		if (isUiClick) return;

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (followNode == null) return;

		bool canBePlaced = CanBePlaced(followNode);
		if (!canBePlaced) return;

	
		SetCandidatePosition(inputPosition);
		_onBuildingPlaced.Execute(_candidateBuilding, followNode);
		_onPreventSelectionChanged.Execute(false);
		ResetCandidateVariables();
	}

	private void CancelLastCandidate(Vector3 inputPosition)
	{
		ResetCandidatePreviousNodes();
		DestroyLastCandidate();
		_onPreventSelectionChanged.Execute(false);
	}



	public bool CanBePlaced(Node followNode)
	{
		var allNodes = GridManager.Instance.GetNodesByBuilding(followNode, _candidateBuilding);

		if (allNodes == null) return false;
		if (allNodes.Count == 0) return false;

		bool isAllNodesAreUnOccupied = allNodes.All(x => !x.IsOccupied);
		if (!isAllNodesAreUnOccupied) return false;


		return true;
	}
}
