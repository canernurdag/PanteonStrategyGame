using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductionUiView : MonoBehaviour
{
	[SerializeField] private ProductionUiPresenter _productionPresenter;

	#region DIRECT REFERENCE SCROLL
	[field: SerializeField] public ScrollRect ScroolRect { get; private set; }
	[field: SerializeField] public RectTransform ViewPortTransform { get; private set; }
	[field: SerializeField] public RectTransform ContentTransform { get; private set; }
	[field: SerializeField] public GridLayoutGroup GridLayoutGroup { get; private set; }

	#endregion


	public void OnBeginDrag(PointerEventData eventData)
	{
		_productionPresenter.OnBeginDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		_productionPresenter.OnDrag(eventData);
	}

}
