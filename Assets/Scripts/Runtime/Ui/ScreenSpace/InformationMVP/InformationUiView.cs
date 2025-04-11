using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationUiView : MonoBehaviour
{
	#region DIRECT REF
	[SerializeField] private Button _productButton;
	[SerializeField] private Button _leftButton;
	[SerializeField] private Button _rightButton;
	[SerializeField] private Image _buildingImage;
	[SerializeField] private Image _productImage;
	[SerializeField] private TMP_Text _buildingNameTMPText;
	[SerializeField] private GameObject _parentProductDivision;
	#endregion

	public void SetProductButtonFunction(Action callback)
	{
		_productButton.onClick.RemoveAllListeners();
		_productButton.onClick.AddListener(()=>callback());
	}

	public void SetLeftButtonFunction(Action callback)
	{
		_leftButton.onClick.RemoveAllListeners();
		_leftButton.onClick.AddListener(()=>callback());
	}

	public void SetRightButtonFunction(Action callback)
	{
		_rightButton.onClick.RemoveAllListeners();
		_rightButton.onClick.AddListener(() => callback());
	}

	public void SetBuildingImage(Sprite sprite)
	{
		_buildingImage.sprite = sprite;
		_buildingImage.preserveAspect = true;
	}

	public void SetProductImage(Sprite sprite)
	{
		_productImage.sprite = sprite;
		_productImage.preserveAspect = true;
	}

	public void SetBuildingName(string name)
	{
		_buildingNameTMPText.text = name;
	}

	public void ActivateProductDivision()
	{
		_parentProductDivision.SetActive(true);
	}

	public void InactivateProductDivision()
	{
		_parentProductDivision.SetActive(false);
	}
}
