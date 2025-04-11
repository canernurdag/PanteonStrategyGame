using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBulletFactory : BulletFactory
{
	[SerializeField] private DefaultBullet _bulletPrefab;
	public override IBullet CreateBullet()
	{
		return LeanPool.Spawn(_bulletPrefab);
	}
}
