using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDataSO", menuName = "ScriptableObjects/BuildingDataSO")]
public class BuildingDataSO : ScriptableObject
{
	public string Name;
	public Sprite Sprite;
	public int DimensionHorizontal;
	public int DimensionVertical;
	public float InitHealth;
}

