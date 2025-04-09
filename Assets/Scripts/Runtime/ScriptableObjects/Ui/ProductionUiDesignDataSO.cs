using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionUiDesignDataSO", menuName = "ScriptableObjects/Ui/ProductionUiDesignDataSO")]
public class ProductionUiDesignDataSO : ScriptableObject
{
	public Vector2 ItemDimensionsInPixels = Vector2.zero;
	public float ItemSpaceX = 50f, ItemSpaceY = 40f;
	public float OutThreshold = 50f;
	public int ColoumnCount = 2;
}
