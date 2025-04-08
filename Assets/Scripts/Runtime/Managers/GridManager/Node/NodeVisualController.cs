using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVisualController : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Sprite _occupiedSprite, _unOccupiedSprite;
	#endregion

	public void SetNodeVisualAsOccupied()
	{
		_spriteRenderer.sprite = _occupiedSprite;
	}

	public void SetNodeVisualAsUnoccupied()
	{
		_spriteRenderer.sprite = _unOccupiedSprite;
	}
}
