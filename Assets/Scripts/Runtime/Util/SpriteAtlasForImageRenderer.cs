using UnityEngine.UI;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasForImageRenderer : MonoBehaviour
{
	[SerializeField] private bool _isAutoFillOn;
	[SerializeField] private Image _image;
	[SerializeField] private SpriteAtlas _atlas;
	[SerializeField] private string _spriteName;

	private void Start()
	{
		if(_isAutoFillOn)
		{
			SetSprite();
		}
	}

	public void SetSprite()
	{
		_image.sprite = _atlas.GetSprite(_spriteName);
	}
}