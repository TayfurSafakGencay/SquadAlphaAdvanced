using System;
using InventorySystem.Item.Types;
using UnityEngine;
using Weapon.Base;

namespace InventorySystem.Item
{
	public class InventoryItemFactory : MonoBehaviour
	{
		[Header("Gun Inventory Item Prefabs")]
		[Space(10)]
		[SerializeField] private GunInventoryItem _commonGunInventoryItemPrefab;
		
		[SerializeField] private GunInventoryItem _rareGunInventoryItemPrefab;
		
		[SerializeField] private GunInventoryItem _epicGunInventoryItemPrefab;
		
		[SerializeField] private GunInventoryItem _legendaryGunInventoryItemPrefab;
		
		[SerializeField] private GunInventoryItem _mythicGunInventoryItemPrefab;
		
		public GunInventoryItem CreateGunInventoryItem(Rarity rarity)
		{
			return rarity switch
			{
				Rarity.Common => Instantiate(_commonGunInventoryItemPrefab),
				Rarity.Rare => Instantiate(_rareGunInventoryItemPrefab),
				Rarity.Epic => Instantiate(_epicGunInventoryItemPrefab),
				Rarity.Legendary => Instantiate(_legendaryGunInventoryItemPrefab),
				Rarity.Mythic => Instantiate(_mythicGunInventoryItemPrefab),
				_ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
			};
		}

		[Space(10)]
		[Header("Attribute Inventory Item Prefabs")]
		[Space(10)]
		
		[SerializeField] private AttributeInventoryItem _commonAttributeInventoryItemPrefab;
		
		[SerializeField] private AttributeInventoryItem _rareAttributeInventoryItemPrefab;
		
		[SerializeField] private AttributeInventoryItem _epicAttributeInventoryItemPrefab;
		
		[SerializeField] private AttributeInventoryItem _legendaryAttributeInventoryItemPrefab;
		
		[SerializeField] private AttributeInventoryItem _mythicAttributeInventoryItemPrefab;
		
		public AttributeInventoryItem CreateAttributeInventoryItem(Rarity rarity)
		{
			return rarity switch
			{
				Rarity.Common => Instantiate(_commonAttributeInventoryItemPrefab),
				Rarity.Rare => Instantiate(_rareAttributeInventoryItemPrefab),
				Rarity.Epic => Instantiate(_epicAttributeInventoryItemPrefab),
				Rarity.Legendary => Instantiate(_legendaryAttributeInventoryItemPrefab),
				Rarity.Mythic => Instantiate(_mythicAttributeInventoryItemPrefab),
				_ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
			};
		}
	}
}