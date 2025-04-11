using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaceable : MonoBehaviour, IPlaceable
{
	#region REF
	public Transform Transform => transform;
	#endregion

	#region INTERNAL VARIABLES
	public bool IsPlaced { get; protected set; }
	#endregion
	public void Place()
	{
		IsPlaced = true;
	}

}
