using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour { }

public class OnGameStateChanged : CanerEvent<GameManager.State> { }
public class OnCreatedGridDataDetermined : CanerEvent<CreatedGridData> { }
public class OnLeftClickInputGiven : CanerEvent<Vector3> { }
public class OnRightClickInputGiven : CanerEvent<Vector3> { }
public class OnMouseWorldPositionGiven : CanerEvent<Vector3> { }
public class OnMouseScreenPositionGiven: CanerEvent<Vector3> { }
public class OnBuildingUiSelected : CanerEvent<BuildingDataSO> { }
public class OnProductCreateRequest : CanerEvent<Building,UnitDataSO> { }
public class OnBuildingSelected : CanerEvent<Building> { }
public class OnBuildingDeselected : CanerEvent { }
public class OnBuildingPlaced : CanerEvent<Building, Node> { }
public class OnUnitSelected : CanerEvent<Unit> { }
public class OnUnitDeselected : CanerEvent { }
public class OnFlagSelected : CanerEvent<FlagSpawnPoint> { }
public class OnFlagDeselected :CanerEvent<FlagSpawnPoint> { }
public class OnDamagableHealthChanged : CanerEvent<DamagableHealthModel> { }
public class OnPreventSelectionChanged : CanerEvent<bool> { }
public class OnUnitMoveCommand : CanerEvent<Unit, Node> { }


