using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : Singleton<PlacementManager>
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
	#endregion

	#region INTERNAL VAR
	private Building _candidatePlacementBuilding;
	private bool _isCandidateExist;
	private List<Node> _placementCandidateBuildingNodes = new();
	#endregion

	private void Start()
	{
		_onBuildingUiSelected = EventManager.Instance.GetEvent<OnBuildingUiSelected>();
		_onMouseWorldPositionGiven = EventManager.Instance.GetEvent<OnMouseWorldPositionGiven>();
		_onLeftClickInputGiven = EventManager.Instance.GetEvent<OnLeftClickInputGiven>();
		_onRightClickInputGiven = EventManager.Instance.GetEvent<OnRightClickInputGiven>();
		_onPreventSelectionChanged = EventManager.Instance.GetEvent<OnPreventSelectionChanged>();

		_onBuildingUiSelected.AddListener(ExecutePlacementCandidateBuildingCreateSequence);
		_onMouseWorldPositionGiven.AddListener(FollowInputPosition);
		_onLeftClickInputGiven.AddListener(HandleLeftClick);
		_onRightClickInputGiven.AddListener(HandleRightClick);
	}


	private void OnDestroy()
	{
		_onBuildingUiSelected.RemoveListener(ExecutePlacementCandidateBuildingCreateSequence);
		_onMouseWorldPositionGiven.RemoveListener(FollowInputPosition);
		_onLeftClickInputGiven.RemoveListener(HandleLeftClick);
		_onRightClickInputGiven.RemoveListener(HandleRightClick);
	}

	public void ExecutePlacementCandidateBuildingCreateSequence(BuildingDataSO selectedBuildingData)
	{
		ClearLastPlacementCandidate();
		Building placementCandidateBuilding = CreateNewPlacementCandidate(selectedBuildingData);
		SetCandiatePlacementBuilding(placementCandidateBuilding);
	}

	private void ClearLastPlacementCandidate()
	{
		if (!_isCandidateExist) return;
		var type = _candidatePlacementBuilding.GetType();
		LeanPool.Despawn(_candidatePlacementBuilding);
		_isCandidateExist = false;
		_candidatePlacementBuilding = null;
	}
	private Building CreateNewPlacementCandidate(BuildingDataSO selectedBuildingData)
	{
		var buildingFactory = GetBuildingFactoryByBuildingType(selectedBuildingData.BuildingType);
		var placementCandidateBuilding = buildingFactory.CreateBuilding();
		return placementCandidateBuilding;
	}
	private BuildingFactory GetBuildingFactoryByBuildingType(BuildingType buildingType)
	{
		return _buildingFactories.FirstOrDefault(x=>x.BuildingType == buildingType);
	}
	public void SetCandiatePlacementBuilding(Building building)
	{
		_candidatePlacementBuilding = building;
		_isCandidateExist = _candidatePlacementBuilding != null;
	}

	private void ResetSelectedBuildingPreviousNodes()
	{
		_placementCandidateBuildingNodes.ForEach(x =>x.ResetNodeVisual());
	}

	public void FollowInputPosition(Vector3 inputPosition)
	{
		if (!_isCandidateExist) return;


		ResetSelectedBuildingPreviousNodes();

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if(followNode == null) return;
		

		var allNodes = GridManager.Instance.GetNodesByBuilding(followNode,_candidatePlacementBuilding);

		if(allNodes == null) return;
		if(allNodes.Count == 0) return;

		_placementCandidateBuildingNodes = allNodes;

		bool isAllNodesAreUnOccupied = IsAllNodesAreUnOccupied(allNodes);
		if (isAllNodesAreUnOccupied)
		{
			_placementCandidateBuildingNodes.ForEach(x => x.SetNodeVisualAsUnoccupied());
		}
		else if (!isAllNodesAreUnOccupied)
		{
			_placementCandidateBuildingNodes.ForEach(x => x.SetNodeVisualAsOccupied());
		}


		_candidatePlacementBuilding.transform.position = followNode.transform.position
			+ Vector3.right * 0.5f
			+ Vector3.up * 0.5f;
	}

	public void HandleLeftClick(Vector3 inputPosition)
	{
		if (!_isCandidateExist) return;

		bool isUiClick = EventSystem.current.IsPointerOverGameObject();
		if (isUiClick) return;

		Node followNode = GridManager.Instance.GetNodeFromWorldPosition(inputPosition);
		if (followNode == null) return;


		var buildingNodes = GridManager.Instance.GetNodesByBuilding(followNode, _candidatePlacementBuilding);

		if (buildingNodes == null) return;
		if (buildingNodes.Count == 0) return;

		bool isAllNodesAreUnOccupied = IsAllNodesAreUnOccupied(buildingNodes);
		if (!isAllNodesAreUnOccupied) return;

		_placementCandidateBuildingNodes = buildingNodes;

	

		_candidatePlacementBuilding.transform.position = followNode.transform.position
			+ Vector3.right * 0.5f
			+ Vector3.up * 0.5f;

		buildingNodes.ForEach(x =>
		{
			x.SetIsOccupied(true);
			x.ResetNodeVisual();
			x.SetInsideSelectable(_candidatePlacementBuilding.BuildingSelectable);
		});

		_isCandidateExist = false;
		_candidatePlacementBuilding = null;

		_placementCandidateBuildingNodes = new();


		_onPreventSelectionChanged.Execute(false);



	}

	private void HandleRightClick(Vector3 inputPosition)
	{
		_onPreventSelectionChanged.Execute(false);
	}
	public bool IsAllNodesAreUnOccupied(List<Node> nodes) 
	{
		return nodes.All(x => !x.IsOccupied);
	}
}
