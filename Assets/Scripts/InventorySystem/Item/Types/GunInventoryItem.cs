using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Item.Types
{
	public class GunInventoryItem : InventoryItem
	{
		[SerializeField] private TextMeshProUGUI _displayNameText;

		public override void SetData(Guid id, string displayName, Sprite icon, GridLayoutGroup gridLayoutGroup)
		{
			base.SetData(id, displayName, icon, gridLayoutGroup);
			
			_displayNameText.text = displayName;
		}
	}
}