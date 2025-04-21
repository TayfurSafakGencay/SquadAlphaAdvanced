using System;
using System.Collections.Generic;
using System.Linq;
using Weapon.Attributes.Base;
using Weapon.Base;

namespace InventorySystem
{
	[UnityEngine.CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory/Create a Inventory", order = 0)]
	public class InventoryData : UnityEngine.ScriptableObject
	{
		private Dictionary<Guid, GunDTO> _gunInventory;
		
		private Dictionary<Guid, AttributeDTO> _attributeInventory;

		#region Get

		public GunDTO GetGun(Guid id)
		{
			if (_gunInventory.TryGetValue(id, out GunDTO gun)) return gun;

			throw new Exception($"Gun with ID {id} not found in inventory.");
		}
		
		public List<GunDTO> GetGuns()
		{
			return _gunInventory.Values.ToList();
		}
		
		public AttributeDTO GetAttribute(Guid id)
		{
			if (_attributeInventory.TryGetValue(id, out AttributeDTO attribute)) return attribute;

			throw new Exception($"Attribute with ID {id} not found in inventory.");
		}
		
		public List<AttributeDTO> GetAttributes()
		{
			return _attributeInventory.Values.ToList();
		}

		#endregion

		#region Save

		public void Save(InventorySaveData inventorySaveData)
		{
			_gunInventory = new Dictionary<Guid, GunDTO>();
			_attributeInventory = new Dictionary<Guid, AttributeDTO>();

			SaveGun(inventorySaveData.GunList);
			SaveAttribute(inventorySaveData.AttributeList);
		}

		private void SaveAttribute(List<AttributeDTO> attributeList)
		{
			foreach (AttributeDTO attribute in attributeList)
			{
				_attributeInventory.Add(attribute.ID, attribute);
			}
		}
		
		private void SaveGun(List<GunDTO> gunList)
		{
			foreach (GunDTO gun in gunList)
			{
				_gunInventory.Add(gun.ID, gun);
			}
		}
		
		#endregion

		#region Load

		public InventorySaveData Load()
		{
			InventorySaveData inventorySaveData = new()
			{
				GunList = _gunInventory.Values.ToList(),
				AttributeList = _attributeInventory.Values.ToList()
			};

			return inventorySaveData;
		}
		
		#endregion
	}
	
	[Serializable]
	public class InventorySaveData
	{
		public List<GunDTO> GunList;
		
		public List<AttributeDTO> AttributeList;
	}
}