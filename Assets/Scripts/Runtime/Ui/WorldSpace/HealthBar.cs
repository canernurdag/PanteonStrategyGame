using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private Image _barImage;
	#endregion

	#region INTERNAL VAR
	private Tween _fillTween;
	#endregion

	public void SetBarImageFillAmount(float amount)
	{
		_barImage.fillAmount = amount;
	}

	public void SetBarImageFillAmount(float amount, float duration, Action callback = null)
	{
		_fillTween?.Kill();
		_fillTween = DOTween.To(
			()=>_barImage.fillAmount,
			x =>_barImage.fillAmount = x,
			amount,
			duration
			).OnComplete(() =>
			{
				callback?.Invoke();
			});
		
	}
}
