using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVisualController : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Sprite _occupiedSprite, _unOccupiedSprite, _placedSprite, _normalSprite;
	#endregion

	public void SetNodeVisualAsNormal()
	{
		_spriteRenderer.sprite = _normalSprite;
	}

	public void SetNodeVisualAsOccupied()
	{
		_spriteRenderer.sprite = _occupiedSprite;
	}

	public void SetNodeVisualAsUnoccupied()
	{
		_spriteRenderer.sprite = _unOccupiedSprite;
	}

	public void SetNodeVisualAsPlaced()
	{
		_spriteRenderer.sprite = _placedSprite;
	}
}
