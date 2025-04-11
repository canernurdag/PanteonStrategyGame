using DG.Tweening;
using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour, IBullet, IPoolable
{
	public Transform Transform => transform;
	private Tween _moveTween;

	public void Throw(Vector3 initPost, Vector3 targetPos, float speed, Action callback)
	{
		_moveTween?.Kill();
		transform.position = initPost;
		_moveTween = transform.DOMove(targetPos, speed)
			.SetSpeedBased()
			.OnComplete(() => 
			{ 
				callback?.Invoke();
				LeanPool.Despawn(this);
			});
	}

	private void OnDisable()
	{
		_moveTween?.Kill();
	}

	public void OnSpawn()
	{
		
	}

	public void OnDespawn()
	{
		_moveTween?.Kill();
	}
}
