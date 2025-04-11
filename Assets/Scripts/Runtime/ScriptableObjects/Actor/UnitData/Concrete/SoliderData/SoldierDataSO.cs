using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierDataSO", menuName = "ScriptableObjects/Actor/Unit/SoldierDataSO")]
public class SoldierDataSO : UnitDataSO
{
	public float InitHealth;
	public float DamagePerAttack;
}
