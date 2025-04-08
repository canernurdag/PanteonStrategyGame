using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour { }

public class OnGameStateChanged : CanerEvent<GameManager.State> { }

public class OnGridCenterPositionDetermined : CanerEvent<Vector3> { }
public class OnLeftClickInputGiven : CanerEvent<Vector2> { }
public class OnRightClickInputGiven : CanerEvent<Vector2> { }
public class OnMousePositionGiven : CanerEvent<Vector2> { }
