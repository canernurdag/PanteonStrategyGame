using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDataSO", menuName = "ScriptableObjects/Actor/BuildingDataSO")]
public class BuildingDataSO : ScriptableObject
{
	public string Name;
	public Sprite Sprite;
	public int DimensionHorizontal;
	public int DimensionVertical;
	public float InitHealth;
	public List<ProductDataSO> ProductDatas = new();
}

