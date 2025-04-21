using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem.Item
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
	{
		public Guid Id;
		public Image Icon;
		
		private CanvasGroup _canvasGroup;
		private RectTransform _rectTransform;
		private Transform _originalParent;
		private GridLayoutGroup _gridLayoutGroup;

		public virtual void SetData(Guid id, string displayName, Sprite icon, GridLayoutGroup gridLayoutGroup)
		{
			_gridLayoutGroup = gridLayoutGroup;
			
			Id = id;
			Icon.sprite = icon;
		}

		protected virtual void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			_rectTransform = GetComponent<RectTransform>();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_gridLayoutGroup.enabled = false;
			
			_originalParent = transform.parent;
			transform.SetParent(transform.root); 
			_canvasGroup.blocksRaycasts = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			_rectTransform.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_gridLayoutGroup.enabled = true;

			_canvasGroup.blocksRaycasts = true;
			transform.SetParent(_originalParent);
			_rectTransform.localPosition = Vector3.zero;
		}

		public virtual void OnDrop(PointerEventData eventData)
		{
			print(gameObject.name);
			
			// Override for items that can receive drops (e.g. guns can accept attributes)
		}
	}
}