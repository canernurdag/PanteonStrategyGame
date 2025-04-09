using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUiModel : MonoBehaviour
{
    [field: SerializeField] public BuildingDataSO BuildingDataSO { get; private set; }

    public void SetBuildingDataSO(BuildingDataSO buildingDataSO)
    {
        BuildingDataSO = buildingDataSO;
    }
}
