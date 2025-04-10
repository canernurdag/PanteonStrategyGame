using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour { }

public class OnGameStateChanged : CanerEvent<GameManager.State> { }

public class OnCreatedGridDataDetermined : CanerEvent<CreatedGridData> { }
public class OnLeftClickInputGiven : CanerEvent<Vector3> { }
public class OnRightClickInputGiven : CanerEvent<Vector3> { }
public class OnMouseWorldPositionGiven : CanerEvent<Vector3> { }
public class OnMouseScreenPositionGive: CanerEvent<Vector3> { }
public class OnBuildingUiSelected : CanerEvent<BuildingDataSO> { }
public class OnProductCreateRequest : CanerEvent<Building,ProductDataSO> { }
public class OnBuildingSelected : CanerEvent<Building> { }
