using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "BuildingDataSO", menuName = "ScriptableObjects/Actor/BuildingDataSO")]
public class BuildingDataSO : ScriptableObject
{
	public BuildingType BuildingType;
	public string Name;
	public SpriteAtlas SpriteAtlas;
	public string SpriteName;
	public int DimensionHorizontal;
	public int DimensionVertical;
	public float InitHealth;
	public List<UnitDataSO> ProductDatas = new();
	public int PreferedSpawnNodeHorizontal, PreferedSpawnNodeVertical;
}

