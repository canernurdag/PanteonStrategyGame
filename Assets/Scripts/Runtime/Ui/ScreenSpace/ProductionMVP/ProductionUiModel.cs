using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionUiModel : MonoBehaviour
{
	#region DATA  
	[field: SerializeField] public ProductionUiDesignDataSO ProductionUiDesignDataSO { get; private set; }
	[field: SerializeField] public ProductionBuildingDataSO ProductionBuildingDataSO { get; set; }
	#endregion
}
