using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDataSO", menuName = "ScriptableObjects/Level/ProductionBuildingDataSO")]

public class ProductionBuildingDataSO : ScriptableObject
{
    public List<BuildingDataSO> ActiveBuildingDatas = new();
}
