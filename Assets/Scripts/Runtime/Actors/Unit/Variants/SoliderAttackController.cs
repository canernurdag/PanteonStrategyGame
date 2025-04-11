using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderAttackController : MonoBehaviour, IAttacker
{
	public Transform Transform => transform;

	public void Attack(float damageAmount)
	{
		throw new System.NotImplementedException();
	}
}
