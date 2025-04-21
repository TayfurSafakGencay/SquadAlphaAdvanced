using InventorySystem.Item;
using InventorySystem.Item.Types;
using Systems.RuntimeData;
using UnityEngine;
using UnityEngine.UI;
using Weapon.Attributes.Base;
using Weapon.Base;
using Weapon.DefaultData;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] private InventoryData _inventoryData;
		
		[SerializeField] private InventoryItemFactory _inventoryItemFactory;
		
		[Header("Containers")]
		
		[SerializeField] private GridLayoutGroup _gunContainer;
		
		[SerializeField] private GridLayoutGroup _attributeContainer;

		private void Awake()
		{
			Init();
		}

		public void Init()
		{
			CreateGuns();
			CreateAttributes();
		}

		#region Create Items

		private async void CreateGuns()
		{
			DefaultGunData defaultGunData = await RuntimeDataManager.Instance.LoadAsync<DefaultGunData>(AddressableKey.DefaultGunData);

			foreach (GunDTO gunDto in _inventoryData.GetGuns())
			{
				GunInventoryItem gun = Instantiate(_inventoryItemFactory.CreateGunInventoryItem(gunDto.Rarity), _gunContainer.transform);
				gun.SetData(gunDto.ID, gunDto.Name, defaultGunData.GetGunData(gunDto.Name).Icon, _gunContainer);
			}
		}
		
		private async void CreateAttributes()
		{
			DefaultAttributeData defaultAttributeData = await RuntimeDataManager.Instance.LoadAsync<DefaultAttributeData>(AddressableKey.DefaultAttributeData);
			
			foreach (AttributeDTO attributeDTO in _inventoryData.GetAttributes())
			{
				AttributeInventoryItem attribute = Instantiate(_inventoryItemFactory.CreateAttributeInventoryItem(attributeDTO.Rarity), _attributeContainer.transform);
				attribute.SetData(attributeDTO.ID, attributeDTO.DisplayName, defaultAttributeData.GetAttributeData(attributeDTO.AttributeType).Icon, _attributeContainer);
			}
		}

		#endregion


	}
}