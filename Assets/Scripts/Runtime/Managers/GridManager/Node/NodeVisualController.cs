using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NodeVisualController : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private SpriteAtlas _atlas;
	[SerializeField] private string _occupiedSprite, _unOccupiedSprite, _placedSprite, _normalSprite;
	#endregion


	public void SetNodeVisualAsNormal()
	{
		_spriteRenderer.sprite = GetSprite(_normalSprite);
	}

	public void SetNodeVisualAsOccupied()
	{
		_spriteRenderer.sprite = GetSprite(_occupiedSprite);
	}

	public void SetNodeVisualAsUnoccupied()
	{
		_spriteRenderer.sprite = GetSprite(_unOccupiedSprite);
	}

	public void SetNodeVisualAsPlaced()
	{
		_spriteRenderer.sprite = GetSprite(_placedSprite);
	}

	private Sprite GetSprite(string name)
	{
		return _atlas.GetSprite(name);
	}
}
