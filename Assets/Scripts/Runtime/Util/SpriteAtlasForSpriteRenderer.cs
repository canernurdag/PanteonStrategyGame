using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasForSpriteRenderer : MonoBehaviour
{
	[SerializeField] private bool _isAutoFillOn;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private SpriteAtlas _atlas;
	[SerializeField] private string _spriteName;

	private void Start()
	{
		if (_isAutoFillOn)
		{
			SetSprite();
		}
	}

	public void SetSprite()
	{
		_spriteRenderer.sprite = _atlas.GetSprite(_spriteName);
	}
}
