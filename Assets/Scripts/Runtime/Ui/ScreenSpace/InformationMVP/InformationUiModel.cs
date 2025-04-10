using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationUiModel : MonoBehaviour
{
	public Building SelectedBuilding { get; private set; }

	public void SetSelectedBuilding(Building building)
	{
		SelectedBuilding = building;
	}
}
