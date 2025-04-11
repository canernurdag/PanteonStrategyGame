using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsAttacker : MonoBehaviour, IAttacker
{
	[SerializeField] private Transform _muzzlePos;
	[SerializeField] private BulletFactory bulletFactory;

	#region INTERNAL
	private float _damageAmount;
	#endregion
	public Transform Transform => transform;

	public void Attack(IDamagable damageable)
	{
		var bullet = bulletFactory.CreateBullet();
		bullet.Throw(_muzzlePos.position,
			damageable.Transform.position,
			10,
			() => damageable.Damage(_damageAmount));
	}

	public void SetDamageAmount(float damageAmount)
	{
		_damageAmount = damageAmount;
	}
}
