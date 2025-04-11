using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAsMovable : MonoBehaviour, IUnitMovable
{
	#region INTERNAL VAR
	private Tween _moveTween;

	public Transform Transform => throw new NotImplementedException();
	#endregion

	public void Move(Vector3 targetPosition, float duration, Action callback = null)
	{
		_moveTween?.Kill();
		_moveTween = transform.DOMove(targetPosition, duration)
			.SetEase(Ease.Linear)
			.OnComplete(() => callback?.Invoke());
	}

	public void Move(Vector3[] path, float duration, Action callback = null)
	{
		_moveTween?.Kill();
		_moveTween = transform.DOPath(path, duration, pathType: PathType.Linear, pathMode:PathMode.Sidescroller2D)
			.SetEase(Ease.Linear)
			.OnComplete(() => callback?.Invoke());
	}
}
