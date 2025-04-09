
using UnityEngine;
using UnityEngine.EventSystems;

public class ProductionUiViewPointerEventDataHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	[SerializeField] private ProductionUiView productionView;
	public void OnBeginDrag(PointerEventData eventData)
	{
		productionView.OnBeginDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		productionView.OnDrag(eventData);
	}
}
