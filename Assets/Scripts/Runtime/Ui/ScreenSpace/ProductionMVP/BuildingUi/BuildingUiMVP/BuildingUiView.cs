using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUiView : MonoBehaviour
{
	#region DIRECT REFERENCE
	[SerializeField] private Image _insideImage;
	[SerializeField] private Button _button;
	[SerializeField] private RectTransform _rectTransform;
	#endregion

	public void SetInsideImage(Sprite sprite)
	{
		_insideImage.sprite = sprite;
		_insideImage.preserveAspect = true;
	}

	public void SetButtonFunction(Action onClick)
	{
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(()=> onClick());
	}

	public Rect GetRect()
	{
		return _rectTransform.rect;
	}
}
