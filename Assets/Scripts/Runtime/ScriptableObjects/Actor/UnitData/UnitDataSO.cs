
using UnityEngine;
using UnityEngine.U2D;

public abstract class UnitDataSO : ScriptableObject
{
	public UnitType UnitType;
	public string Name;
	public SpriteAtlas SpriteAtlas;
	public string SpriteName;
}
