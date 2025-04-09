using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierDataSO", menuName = "ScriptableObjects/Actor/SoldierDataSO")]
public class SoldierDataSO : ScriptableObject
{
	public string Name;
	public Sprite Sprite;
	public float InitHealth;
	public float DamagePerAttack;
}
