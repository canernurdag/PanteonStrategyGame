
using UnityEngine;
using UnityEngine.U2D;

public class UnitDataSO : ScriptableObject
{
	public UnitType UnitType;
	public string Name;
	public SpriteAtlas SpriteAtlas;
	public string SpriteName;
	public float InitHealth;
	public float DamagePerAttack;
}

