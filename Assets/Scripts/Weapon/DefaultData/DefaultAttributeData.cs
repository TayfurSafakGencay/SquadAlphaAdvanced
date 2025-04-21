using System.Collections.Generic;
using UnityEngine;
using Weapon.Attributes.Base;

namespace Weapon.DefaultData
{
	[CreateAssetMenu(fileName = "DefaultAttributeData", menuName = "Scriptable Objects/Gun/Attribute/Create a Attribute Data", order = 0)]
	public class DefaultAttributeData : ScriptableObject
	{
		[SerializeField]
		private List<GunAttribute>_attributes;

		public Dictionary<AttributeType, GunAttribute> AttributeType;
		
		private bool _isInitialized;

		private void OnEnable()
		{
			_isInitialized = false;

			Initialize();
		}
		
		public void Initialize()
		{
			if (_isInitialized) return;
			_isInitialized = true;

			AttributeType = new Dictionary<AttributeType, GunAttribute>();

			foreach (GunAttribute attribute in _attributes)
			{
				AttributeType.TryAdd(attribute.AttributeDto.AttributeType, attribute);
			}
		}

		public AttributeDTO GetAttributeData(AttributeType attributeType)
		{
			if (AttributeType.TryGetValue(attributeType, out GunAttribute data)) return data.AttributeDto;

			Debug.LogWarning($"Gun with name {attributeType} not found!");
			return new AttributeDTO();
		}
	}
}