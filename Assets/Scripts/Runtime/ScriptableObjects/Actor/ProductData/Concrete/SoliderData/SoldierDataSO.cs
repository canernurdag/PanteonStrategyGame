using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierDataSO", menuName = "ScriptableObjects/Actor/Product/SoldierDataSO")]
public class SoldierDataSO : ProductDataSO
{
	public float InitHealth;
	public float DamagePerAttack;
}
