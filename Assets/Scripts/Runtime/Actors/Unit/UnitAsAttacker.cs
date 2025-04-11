using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsAttacker : MonoBehaviour, IAttacker
{
	[SerializeField] private Transform _muzzlePos;
	[SerializeField] private BulletFactory bulletFactory;
	[SerializeField] private UnitDataSO _unitDataSO;

	public Transform Transform => transform;

	public void Attack(IDamagable damageable)
	{
		var bullet = bulletFactory.CreateBullet();
		bullet.Throw(_muzzlePos.position,
			damageable.Transform.position,
			_unitDataSO.AttackSpeed,
			() => damageable.Damage(_unitDataSO.DamagePerAttack));
	}

}
